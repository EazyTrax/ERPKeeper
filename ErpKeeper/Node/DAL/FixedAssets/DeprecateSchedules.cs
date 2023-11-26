using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Transactions.Commercials;
using ERPKeeper.Node.Models.Assets;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.DAL.Assets
{
    public class DeprecateSchedules : ERPNodeDalRepository
    {
        public DeprecateSchedules(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.DeprecateSchedule;
        }

        public IQueryable<DeprecateSchedule> GetIQueryable()
        {
            return erpNodeDBContext.DeprecateSchedules;
        }

        public List<DeprecateSchedule> GetAll() => erpNodeDBContext.DeprecateSchedules.ToList();


        public DeprecateSchedule Find(Guid transactionUid) => erpNodeDBContext.DeprecateSchedules.Find(transactionUid);
        public void Clean()
        {
            //Remove null Schedule
            var nullSchedule = erpNodeDBContext.DeprecateSchedules.Where(s => s.FixedAssetUid == null);
            erpNodeDBContext.DeprecateSchedules.RemoveRange(nullSchedule);
            erpNodeDBContext.SaveChanges();
        }

        public DeprecateSchedule Save(DeprecateSchedule fixedAssetSchedule)
        {
            var existDepreciationSchedule = erpNodeDBContext.DeprecateSchedules.Find(fixedAssetSchedule.Uid);


            if (existDepreciationSchedule != null)
            {
                existDepreciationSchedule.FiscalYear = organization.FiscalYears.Find(existDepreciationSchedule.EndDate);

                if (existDepreciationSchedule.PostStatus == LedgerPostStatus.Locked)
                    return existDepreciationSchedule;

                erpNodeDBContext.SaveChanges();
                return existDepreciationSchedule;
            }
            else
            {
                fixedAssetSchedule.FiscalYear = organization.FiscalYears.Find(existDepreciationSchedule.EndDate);

                erpNodeDBContext.DeprecateSchedules.Add(fixedAssetSchedule);
                erpNodeDBContext.SaveChanges();

                return fixedAssetSchedule;
            }
        }
   

        public void UnPost()
        {
            Console.WriteLine("> Un Post" + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            erpNodeDBContext.DeprecateSchedules.ToList().ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }


        public List<DeprecateSchedule> ReadyForPost
        {
            get
            {
                organization.FixedAssets.RemoveNullSchedule();
                DateTime firstPostDate = organization.DataItems.FirstDate;

                return erpNodeDBContext.DeprecateSchedules
                    .Where(s => s.PostStatus == LedgerPostStatus.Editable)
                    .Where(s => s.StartDate >= firstPostDate)
                    .Where(s => s.EndDate <= DateTime.Now)
                    .ToList();
            }
        }


        public void PostLedger()
        {
            var postingTransactions = this.ReadyForPost;

            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            postingTransactions.ForEach(s =>
            {
                this.PostLedger(s, false);
            });

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }


        public void UnPost(DeprecateSchedule fixedAssetSchedule)
        {
            organization.LedgersDal.RemoveTransaction(fixedAssetSchedule.Uid);
            fixedAssetSchedule.PostStatus = LedgerPostStatus.Editable;
        }


        public bool PostLedger(DeprecateSchedule tr, bool SaveImmediately = true)
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


            trLedger.Debit(tr.FixedAsset.FixedAssetType.AmortizeExpenseAccount, tr.DepreciationValue);
            trLedger.Credit(tr.FixedAsset.FixedAssetType.AccumulateDeprecateAcc, tr.DepreciationValue);

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

    }
}