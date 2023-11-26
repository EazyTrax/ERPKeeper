
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Financial.Lends;
using ERPKeeper.Node.Models.Financial.Transfer;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.DAL.Financial
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
            var existLend = erpNodeDBContext.Lends.Find(lend.Uid);

            if (existLend == null)
                erpNodeDBContext.Lends.Add(lend);
            else
            {
                if (existLend.PostStatus == LedgerPostStatus.Locked)
                    return;

                existLend.TrnDate = lend.TrnDate;
                existLend.AssetAccountGuid = lend.AssetAccountGuid;
                existLend.LendingAssetAccountGuid = lend.LendingAssetAccountGuid;
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

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            postingTransactions.ForEach(s =>
            {
                this.PostLedger(s, false);
            });

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
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
                Uid = tr.Uid,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };

            trLedger.Credit(tr.AssetAccount, tr.Amount);
            trLedger.Debit(tr.LendingAssetAccount, tr.Amount);


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
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "UPDATE [dbo].[ERP_Finance_Lends] SET  [PostStatus] = '0'";
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

        }
        public void UnPost(Lend lend)
        {
            organization.LedgersDal.RemoveTransaction(lend.Uid);
            lend.PostStatus = LedgerPostStatus.Editable;

            erpNodeDBContext.SaveChanges();
        }


    }
}