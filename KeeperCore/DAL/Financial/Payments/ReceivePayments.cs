using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Transactions.Commercials;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial.Payments;
using KeeperCore.ERPNode.Models.Financial.Payments.Enums;

namespace KeeperCore.ERPNode.DAL.Financial
{
    public class ReceivePayments : ERPNodeDalRepository
    {
        public ReceivePayments(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.ReceivePayment;
        }
        public IQueryable<ReceivePayment> GetQueryable()
        {
            return erpNodeDBContext.ReceivePayments;
        }
        public List<ReceivePayment> GetAll()
        {
            return erpNodeDBContext.ReceivePayments.ToList();
        }
        public void Reorder()
        {
            var payments = this.GetQueryable()
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
        public int GetNextNumber() => (erpNodeDBContext.ReceivePayments.Max(e => (int?)e.No) ?? 0) + 1;
        public int GetNextMonthlyNo(DateTime trDate)
        {
            return (erpNodeDBContext.ReceivePayments
                .Where(s => s.TrnDate.Month == trDate.Month && s.TrnDate.Year == trDate.Year)
                .Max(e => (int?)e.NoInMonth) ?? 0) + 1;
        }
        public ReceivePayment Create(Models.Profiles.Profile profile, Commercial com, DateTime? workingDate)
        {
            var payment = new ReceivePayment()
            {
                Id = Guid.NewGuid(),
                TrnDate = (DateTime)(workingDate ?? DateTime.Now),
                Profile = profile,
                AssetAccount = organization.SystemAccounts.Cash,
                TransactionType = ERPObjectType.ReceivePayment,
                No = this.GetNextNumber(),
                NoInMonth = GetNextMonthlyNo((DateTime)(workingDate ?? DateTime.Now)),
                CommercialAmount = com.Total,
                CommercialBeforeTaxAmount = com.LinesTotal
            };

            erpNodeDBContext.ReceivePayments.Add(payment);
            erpNodeDBContext.SaveChanges();

            //Add commercial and Update Commercial
            payment.AddCommercial(com);
            erpNodeDBContext.SaveChanges();
            return payment;
        }
        public ReceivePayment Save(ReceivePayment payment)
        {
            var existPayment = erpNodeDBContext.ReceivePayments.Find(payment.Id);

            if (existPayment == null)
                return payment;

            if (existPayment.PostStatus == LedgerPostStatus.Locked)
                return existPayment;

            existPayment.TransactionType = this.transactionType;
            existPayment.TrnDate = payment.TrnDate;
            existPayment.Delay = payment.Delay;
            existPayment.RetentionTypeId = payment.RetentionTypeId;
            existPayment.RetentionType = this.organization.RetentionTypes.Find(payment.RetentionTypeId ?? Guid.Empty);

            existPayment.DiscountAmount = payment.DiscountAmount;
            existPayment.BankFeeAmount = payment.BankFeeAmount;
            existPayment.AssetAccountId = payment.AssetAccountId;
            existPayment.Memo = payment.Memo;



            existPayment.UpdateName();
            existPayment.CalculateAmount();
            erpNodeDBContext.SaveChanges();
            return existPayment;


        }
        public bool Delete(Guid id)
        {
            var payment = erpNodeDBContext.ReceivePayments.Find(id);
            if (payment.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Payment Locked.");
            else if (payment.Commercials.Count > 0)
                throw new Exception("Commercials Count > 0.");
            else
                erpNodeDBContext.ReceivePayments.Remove(payment);

            erpNodeDBContext.SaveChanges();
            return true;
        }

        public ReceivePayment Find(Guid id) => erpNodeDBContext.ReceivePayments.Find(id);
        public List<ReceivePayment> FindList(Guid id) => erpNodeDBContext.ReceivePayments
                                    .Where(s => s.Id == id)
                                    .ToList();
        private List<ReceivePayment> GetReadyForPost()
        {
            return erpNodeDBContext.ReceivePayments
.Where(s => s.PostStatus == LedgerPostStatus.Editable)
.ToList();
        }

        public void PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();
            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;

            postingTransactions.ForEach(s => this.PostLedger(s, false));

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        public bool PostLedger(ReceivePayment tr, bool SaveImmediately = true)
        {

            if (tr.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Posted Transaction");


            tr.AssetAccount = tr.AssetAccount ?? organization.SystemAccounts.Cash;
            tr.ReceivableAccount = organization.SystemAccounts.AccountReceivable;
            tr.CalculateAmount();

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType,
                ProfileName = tr.Profile?.Name,
            };

            string memo = "";
            //Cr.
            trLedger.Debit(tr.AssetAccount, tr.AmountAfterDiscountAndRetention, memo);
            trLedger.Debit(organization.SystemAccounts.DiscountGiven, tr.DiscountAmount, memo);
            trLedger.Debit(tr.RetentionType?.RetentionToAccount, tr.AmountRetention, memo);
            trLedger.Credit(tr.ReceivableAccount, tr.Amount, memo);




            //BankFee
            trLedger.Debit(organization.SystemAccounts.BankFee, tr.BankFeeAmount);
            trLedger.Credit(tr.AssetAccount, tr.BankFeeAmount);


            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }
            else if (tr.PostStatus != LedgerPostStatus.Locked)
            {
                return false;
            }

            return true;


        }
        public void UnPost(ReceivePayment financialPayment)
        {
            Console.WriteLine("> Un Posting,");
            organization.LedgersDal.RemoveTransaction(financialPayment.Id);
            financialPayment.PostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
        }
        public void UnPost()
        {
            organization.LedgersDal.UnPost(this.transactionType);

            string sqlCommand = "UPDATE [dbo].[ERP_Financial_General_Payments] SET  [PostStatus] = '0' WHERE Discriminator = '{0}'";
            sqlCommand = string.Format(sqlCommand, transactionType.ToString());
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
            erpNodeDBContext.SaveChanges();
        }
    }
}
