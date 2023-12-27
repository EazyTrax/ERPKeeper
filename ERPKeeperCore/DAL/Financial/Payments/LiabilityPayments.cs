using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Financial.Payments.Enums;
using KeeperCore.ERPNode.Models.Profiles;
using KeeperCore.ERPNode.Models.Transactions;
using KeeperCore.ERPNode.Models.Transactions.Commercials;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Financial
{
    public class LiabilityPayments : ERPNodeDalRepository
    {
        public LiabilityPayments(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.LiabilityPayment;
        }

        public LiabilityPayment Find(Guid id) => erpNodeDBContext.LiabilityPayments.Find(id);

        public int GetNextNumber() => (erpNodeDBContext.LiabilityPayments.Max(e => (int?)e.No) ?? 0) + 1;
        public int GetNextMonthlyNo(DateTime trDate)
        {
            return (erpNodeDBContext.LiabilityPayments
                .Where(s => s.TrnDate.Month == trDate.Month && s.TrnDate.Year == trDate.Year)
                .Max(e => (int?)e.NoInMonth) ?? 0) + 1;
        }

        public bool PostLedger(LiabilityPayment tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            if (tr.LiabilityAccount == null)
                return false;


            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };

            //Dr.
            trLedger.Debit(tr.LiabilityAccount, tr.Amount);

            //Cr.
            trLedger.Credit(tr.RetentionType?.RetentionToAccount, tr.AmountRetention);
            trLedger.Credit(tr.OptionalAssetAccount, tr.AmountPayFromOptionalAcc);
            trLedger.Credit(tr.AssetAccount, tr.AmountPayFromPrimaryAcc);


            if (tr.BankFeeAmount > 0)
            {
                trLedger.Debit(organization.SystemAccounts.BankFee, tr.BankFeeAmount);
                trLedger.Credit(tr.AssetAccount, tr.BankFeeAmount);
            }

            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }
            else
                return false;

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();
            return true;
        }

        public LiabilityPayment Copy(LiabilityPayment originalLiabilityPayment, DateTime trDate)
        {
            var cloneLiabilityPayment = this.erpNodeDBContext.LiabilityPayments
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == originalLiabilityPayment.Id);

            cloneLiabilityPayment.Id = Guid.NewGuid();
            cloneLiabilityPayment.TrnDate = trDate;
            cloneLiabilityPayment.No = organization.LiabilityPayments.GetNextNumber();
            cloneLiabilityPayment.PostStatus = LedgerPostStatus.Editable;

            this.erpNodeDBContext.LiabilityPayments.Add(cloneLiabilityPayment);
            this.erpNodeDBContext.SaveChanges();

            return cloneLiabilityPayment;
        }


        public LiabilityPayment CreateNew(Guid liabilityAccountId, decimal amount)
        {
            var LiabilityPayment = new LiabilityPayment()
            {
                Id = Guid.NewGuid(),
                LiabilityAccountId = liabilityAccountId,
                TrnDate = DateTime.Now,
                LiabilityAmount = amount,
                AssetAccount = organization.SystemAccounts.Cash,
                TransactionType = ERPObjectType.LiabilityPayment,
            };


            erpNodeDBContext.LiabilityPayments.Add(LiabilityPayment);
            erpNodeDBContext.SaveChanges();

            return LiabilityPayment;
        }

        public bool Remove(LiabilityPayment payment)
        {
            if (payment.PostStatus != LedgerPostStatus.Locked)
            {
                erpNodeDBContext.LiabilityPayments.Remove(payment);
                erpNodeDBContext.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Posted Transaction");
            }
        }

        public void ReOrder()
        {
            var payments = this.erpNodeDBContext.LiabilityPayments
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
                payment.NoInMonth = this.GetNextMonthlyNo(payment.TrnDate);
                payment.UpdateName();
                this.erpNodeDBContext.SaveChanges();
            }
        }

        public LiabilityPayment Save(LiabilityPayment payment)
        {
            var existPayment = erpNodeDBContext.LiabilityPayments.Find(payment.Id);

            if (existPayment == null && payment.LiabilityAccountId != null)
            {
                existPayment = organization.LiabilityPayments.CreateNew((Guid)payment.LiabilityAccountId, payment.Amount);
                erpNodeDBContext.SaveChanges();
                return payment;
            }


            else if (existPayment != null)
            {
                if (existPayment.PostStatus == LedgerPostStatus.Locked)
                    return existPayment;

                existPayment.TrnDate = payment.TrnDate;
                existPayment.LiabilityAccountId = payment.LiabilityAccountId;
                existPayment.LiabilityAmount = payment.LiabilityAmount;
                existPayment.BankFeeAmount = 0;

                erpNodeDBContext.SaveChanges();

                return existPayment;
            }
            return null;
        }

        public LiabilityPayment Create(Profile profile)
        {
            var payment = new LiabilityPayment()
            {
                TrnDate = DateTime.Now,
            };

            erpNodeDBContext.LiabilityPayments.Add(payment);

            return payment;
        }

        private List<LiabilityPayment> GetReadyForPost()
        {
            return erpNodeDBContext.LiabilityPayments
                    .Where(s => s.PostStatus == LedgerPostStatus.Editable)
                    .ToList();
        }

        public List<LiabilityPayment> GetAll()
        {
            return this.erpNodeDBContext.LiabilityPayments.ToList();
        }

        public void PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();
            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            postingTransactions.ForEach(s =>
            {
                this.PostLedger(s, false);
            });
            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }

        public void UnPost(LiabilityPayment LiabilityPayment)
        {
            Console.WriteLine("> Un Posting,");
            organization.LedgersDal.RemoveTransaction(LiabilityPayment.Id);
            LiabilityPayment.PostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
        }

        public void UnPost()
        {
            Console.WriteLine("> Un Post " + transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);


            sqlCommand = "UPDATE [dbo].[ERP_Financial_General_Payments] SET  [PostStatus] = '0' WHERE Discriminator = '{0}'";
            sqlCommand = string.Format(sqlCommand, transactionType.ToString());
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
            erpNodeDBContext.SaveChanges();
        }

    }
}
