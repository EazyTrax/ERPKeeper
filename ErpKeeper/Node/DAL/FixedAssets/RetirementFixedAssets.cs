using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Transactions.Commercials;
using ERPKeeper.Node.Models.Assets;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Assets.Enums;
using ERPKeeper.Node.Models.Accounting.Enums;
using Newtonsoft.Json;

namespace ERPKeeper.Node.DAL.Assets
{
    public class RetirementFixedAssets : ERPNodeDalRepository
    {
        public RetirementFixedAssets(Organization organization) : base(organization)
        {
            transactionType = Models.Accounting.Enums.ERPObjectType.FixedAssetRetirement;
        }


        public List<FixedAsset> GetListAll() =>
            erpNodeDBContext.FixedAssets
            .Where(f => f.RetirementDate != null)
            .OrderBy(a => a.StartDeprecationDate)
            .ToList();

        public FixedAsset Find(Guid uid) => erpNodeDBContext.FixedAssets.Find(uid);


        public void UpdateStatus()
        {
            this.GetListAll()
                .Where(f => DateTime.Today > f.RetirementDate)
                .ToList()
                .ForEach(fixedAsset =>
                {
                    fixedAsset.Status = FixedAssetStatus.Retried;
                });

            erpNodeDBContext.SaveChanges();
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

            erpNodeDBContext.FixedAssets.ToList()
                .ForEach(s => s.RetirementPostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }

        public List<FixedAsset> ReadyForPost
        {
            get
            {
                var firstDate = organization.DataItems.FirstDate;

                return erpNodeDBContext.FixedAssets
                    .Where(s => s.RetirementDate >= firstDate)
                    .Where(s => s.RetirementPostStatus == LedgerPostStatus.Editable)
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
                this.PostRetirementLedger(s, false);
            });

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();


        }
        public void UnPost(FixedAsset fixedAsset)
        {
            if (fixedAsset.RetirementUid == null)
                return;

            organization.LedgersDal.RemoveTransaction((Guid)fixedAsset.RetirementUid);
            fixedAsset.RetirementPostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
        }

        public bool PostRetirementLedger(FixedAsset tr, bool SaveImmediately = true)
        {


            if (tr.RetirementDate == null)
                return false;

            if (tr.RetirementPostStatus == LedgerPostStatus.Locked)
                return false;

            if (tr.RetirementUid == null)
                tr.RetirementUid = Guid.NewGuid();


            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Uid = (Guid)tr.RetirementUid,
                TrnDate = (DateTime)tr.RetirementDate,
                TransactionName = tr.Name + " Retirement",
                TransactionNo = tr.No,
                TransactionType = transactionType
            };

            string jsonString = JsonConvert.SerializeObject(trLedger);

            Console.WriteLine(jsonString);


            trLedger.Credit(tr.FixedAssetType.AwaitDeprecateAccount, tr.AssetValue);
            trLedger.Debit(tr.FixedAssetType.AccumulateDeprecateAcc, tr.AssetValue);

            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.RetirementPostStatus = LedgerPostStatus.Locked;
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