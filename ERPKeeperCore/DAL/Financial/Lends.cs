
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Financial;


namespace KeeperCore.ERPNode.DAL.Financial
{
    public class Lends : ERPNodeDalRepository
    {
        public Lends(Organization organization) : base(organization)
        {
            transactionType = Models.Accounting.Enums.ERPObjectType.Lend;
        }

        public List<Lend> GetAll()
        {
            return erpNodeDBContext.Lends.ToList();
        }

        public List<Lend> GetReadyForPost()
        {
            return erpNodeDBContext.Lends
.Where(s => s.PostStatus == LedgerPostStatus.Editable)
.ToList();
        }

        public Lend Find(Guid id) => erpNodeDBContext.Lends.Find(id);

        private int GetNextNumber()
        {
            return (erpNodeDBContext.Lends.Max(e => (int?)e.No) ?? 0) + 1;
        }

        public void Add(Lend entity)
        {
            erpNodeDBContext.Lends.Add(entity);
            erpNodeDBContext.SaveChanges();
        }

        public void ReOrder()
        {
            var lends = erpNodeDBContext.Lends
                .OrderBy(t => t.TrnDate)
                .ToList();

            int i = 1;
            foreach (var lend in lends)
            {
                lend.No = i;
                i++;

            }

            erpNodeDBContext.SaveChanges();

        }


        public void Issue(Lend lend)
        {
            erpNodeDBContext.SaveChanges();
        }

        public void Save(Lend lend)
        {
            var existLend = erpNodeDBContext.Lends.Find(lend.Id);

            if (existLend == null)
                erpNodeDBContext.Lends.Add(lend);
            else
            {
                if (existLend.PostStatus == LedgerPostStatus.Locked)
                    return;

                existLend.TrnDate = lend.TrnDate;
                existLend.CreditFrom_AssetAccountId = lend.CreditFrom_AssetAccountId;
                existLend.DebitTo_AssetAccountId = lend.DebitTo_AssetAccountId;
                existLend.Amount = lend.Amount;
            }

            erpNodeDBContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var Transfer = erpNodeDBContext.Lends.Find(id);
            erpNodeDBContext.Lends.Remove(Transfer);
            this.SaveChanges();
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

            //Console.WriteLine("");
        }
        public bool PostLedger(Lend tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };

            trLedger.Credit(tr.CreditFrom_AssetAccount, tr.Amount);
            trLedger.Debit(tr.DebitTo_AssetAccount, tr.Amount);


            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }
            else
            {
                return false;
            }

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
        }

        public void UnPost()
        {
            Console.WriteLine("> Un Post " + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "UPDATE [dbo].[ERP_Finance_Lends] SET  [PostStatus] = '0'";
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

        }
        public void UnPost(Lend lend)
        {
            organization.LedgersDal.RemoveTransaction(lend.Id);
            lend.PostStatus = LedgerPostStatus.Editable;

            erpNodeDBContext.SaveChanges();
        }


    }
}