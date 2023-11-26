using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.Node.Models.Financial.Payments.Enums;
using ERPKeeper.Node.Models.Profiles;
using ERPKeeper.Node.Models.Transactions;
using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ERPKeeper.Node.DAL.Financial
{
    public class SupplierPayments : ERPNodeDalRepository
    {
        public SupplierPayments(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.SupplierPayment;
        }

        public List<SupplierPayment> GetAll()
        {
            return erpNodeDBContext.SupplierPayments.ToList();
        }

        public bool PostLedger(SupplierPayment tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            tr.AssetAccount = tr.AssetAccount ?? organization.SystemAccounts.Cash;
            tr.LiabilityAccount = tr.LiabilityAccount ?? organization.SystemAccounts.AccountPayable;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Uid = tr.Uid,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType,
                ProfileName = tr.Profile.DisplayName,
            };

            tr.LiabilityAccount = organization.SystemAccounts.AccountPayable;
            //DR.
            trLedger.Debit(tr.LiabilityAccount, tr.Amount);
            //CR.
            trLedger.Credit(organization.SystemAccounts.DiscountTaken, tr.DiscountAmount);
            trLedger.Credit(tr.AssetAccount, tr.TotalBillPaymentAmount);
            trLedger.Credit(tr.RetentionType?.RetentionToAccount, tr.AmountRetention);


            //BANKING.
            trLedger.Debit(organization.SystemAccounts.BankFee, tr.BankFeeAmount);
            trLedger.Credit(tr.AssetAccount, tr.BankFeeAmount);


            if (trLedger.FinalValidate())
            {
                tr.PostStatus = LedgerPostStatus.Locked;
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
            }
            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();
            return true;
        }

        public List<SupplierPayment> FindList(Guid id) => erpNodeDBContext.SupplierPayments
                                     .Where(s => s.Uid == id)
                                     .ToList();

        public SupplierPayment CreateNew(Guid? id)
        {
            var payment = new SupplierPayment()
            {
                Uid = Guid.NewGuid(),
                TrnDate = DateTime.Now,
                LiabilityAccountUid = id,
                AssetAccount = organization.SystemAccounts.Cash
            };

            erpNodeDBContext.SupplierPayments.Add(payment);

            return payment;
        }

        public void RunCheck()
        {
            var payments = erpNodeDBContext.SupplierPayments.ToList();
            payments.ForEach(p => p.CalculateAmount());

            erpNodeDBContext.SaveChanges();
        }



        public bool Delete(Guid id)
        {
            var payment = erpNodeDBContext.SupplierPayments.Find(id);
            if (payment.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Payment Locked.");
            else if (payment.Commercials.Count > 0)
                throw new Exception("Commercials Count > 0.");
            else
                erpNodeDBContext.SupplierPayments.Remove(payment);

            erpNodeDBContext.SaveChanges();
            return true;

        }
        public SupplierPayment Find(Guid id) => erpNodeDBContext.SupplierPayments.Find(id);

        public SupplierPayment Save(SupplierPayment payment)
        {
            var existPayment = erpNodeDBContext.SupplierPayments.Find(payment.Uid);

            if (existPayment.PostStatus == LedgerPostStatus.Locked)
                return existPayment;

            existPayment.TrnDate = payment.TrnDate;
            existPayment.AmountRetention = payment.AmountRetention;
            existPayment.LiabilityAccountUid = payment.LiabilityAccountUid;
            existPayment.DiscountAmount = payment.DiscountAmount;
            existPayment.BankFeeAmount = payment.BankFeeAmount;
            existPayment.AssetAccountUid = payment.AssetAccountUid;
            existPayment.RetentionTypeGuid = payment.RetentionTypeGuid;
            existPayment.Memo = payment.Memo;
            existPayment.UpdateName();
            existPayment.CalculateAmount();

            erpNodeDBContext.SaveChanges();
            return existPayment;
        }

        public void Reorder()
        {
            var payments = this.erpNodeDBContext.SupplierPayments
                    .OrderBy(t => t.TrnDate)
                    .ThenBy(t => t.No)
                    .ToList();

            int i = 1;

            foreach (var payment in payments)
            {
                payment.NoInMonth = 0;
            }
            this.erpNodeDBContext.SaveChanges();

            foreach (var payment in payments)
            {
                payment.No = i++;
                payment.NoInMonth = GetNextMonthlyNo(payment.TrnDate);
                payment.UpdateName();
                this.erpNodeDBContext.SaveChanges();
            }

        }




        public int GetNextNumber() => (erpNodeDBContext.SupplierPayments.Max(e => (int?)e.No) ?? 0) + 1;
        public int GetNextMonthlyNo(DateTime trDate)
        {
            return (erpNodeDBContext.SupplierPayments
                .Where(s => s.TrnDate.Month == trDate.Month && s.TrnDate.Year == trDate.Year)
                .Max(e => (int?)e.NoInMonth) ?? 0) + 1;
        }

        public SupplierPayment Create(Profile profile, Commercial com, DateTime? WorkingDate)
        {
            if (com == null)
                return null;

            var payment = new SupplierPayment()
            {
                Uid = Guid.NewGuid(),
                TrnDate = (DateTime)(WorkingDate ?? DateTime.Now),
                Profile = profile,
                AssetAccount = organization.SystemAccounts.Cash,
                TransactionType = ERPObjectType.SupplierPayment,
                No = GetNextNumber(),
                NoInMonth = GetNextMonthlyNo((DateTime)(WorkingDate ?? DateTime.Now)),
                CommercialAmount = com.Total,
                CommercialBeforeTaxAmount = com.LinesTotal
            };

            erpNodeDBContext.SupplierPayments.Add(payment);
            erpNodeDBContext.SaveChanges();

            payment.AddCommercial(com);
            erpNodeDBContext.SaveChanges();

            return payment;
        }

        private List<SupplierPayment> GetReadyForPost()
        {
            return erpNodeDBContext.SupplierPayments
.Where(s => s.PostStatus == LedgerPostStatus.Editable)
.ToList();
        }

        public void PostLedger()
        {
            var postingTransactions = GetReadyForPost();
            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");
            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            postingTransactions.ForEach(s => PostLedger(s, false));
            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();


        }
        public void UnPost(SupplierPayment payment)
        {
            if (payment.PostStatus == LedgerPostStatus.Editable)
                return;

            Console.WriteLine("> Un Posting,");
            organization.LedgersDal.RemoveTransaction(payment.Uid);
            payment.PostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
        }

        public void UnPost()
        {
            Console.WriteLine("> Un Post " + transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);


            sqlCommand = "UPDATE [dbo].[ERP_Financial_General_Payments] SET  [PostStatus] = '0' WHERE Discriminator = '{0}'";
            sqlCommand = string.Format(sqlCommand, transactionType.ToString());
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);
            erpNodeDBContext.SaveChanges();
        }
    }
}
