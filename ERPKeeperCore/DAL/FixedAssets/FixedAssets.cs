using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Transactions.Commercials;
using KeeperCore.ERPNode.Models.Assets;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Assets.Enums;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore;

namespace KeeperCore.ERPNode.DAL.Assets
{
    public class FixedAssets : ERPNodeDalRepository
    {
        public FixedAssets(Organization organization) : base(organization)
        {
            transactionType = Models.Accounting.Enums.ERPObjectType.FixedAsset;
        }

        public IQueryable<Asset> Query() => erpNodeDBContext.FixedAssets;

        public List<Asset> GetListAll() => erpNodeDBContext.FixedAssets.OrderBy(a => a.StartDeprecationDate).ToList();


        public Asset Find(Guid uid) => erpNodeDBContext.FixedAssets.Find(uid);
        public Asset Save(Asset fixedAsset)
        {
            var exFixedAsset = erpNodeDBContext.FixedAssets.Find(fixedAsset.Id);

            if (exFixedAsset != null)
            {
                if (exFixedAsset.PostStatus == LedgerPostStatus.Locked)
                    return exFixedAsset;

                exFixedAsset.FiscalYear = organization.FiscalYears.Find(fixedAsset.StartDeprecationDate);
                exFixedAsset.Reference = fixedAsset.Reference;
                exFixedAsset.Memo = fixedAsset.Memo;
                exFixedAsset.Name = fixedAsset.Name;
                exFixedAsset.Code = fixedAsset.Code;
                exFixedAsset.FixedAssetTypeId = fixedAsset.FixedAssetTypeId;
                exFixedAsset.StartDeprecationDate = fixedAsset.StartDeprecationDate;
                exFixedAsset.AssetValue = fixedAsset.AssetValue;
                exFixedAsset.SavageValue = fixedAsset.SavageValue;


                erpNodeDBContext.SaveChanges();
                return exFixedAsset;
            }
            else
            {
                fixedAsset.FiscalYear = organization.FiscalYears.Find(fixedAsset.StartDeprecationDate);
                erpNodeDBContext.FixedAssets.Add(fixedAsset);
                erpNodeDBContext.SaveChanges();

                return fixedAsset;
            }
        }

        public void Remove(Asset depAsset)
        {
            erpNodeDBContext.FixedAssets.Remove(depAsset);
            erpNodeDBContext.SaveChanges();
        }

        public List<Asset> ListByStatus(FixedAssetStatus status) => erpNodeDBContext.FixedAssets.Where(d => d.Status == status).ToList();

        public void UpdateStatus()
        {
            this.GetListAll().ForEach(fixedAsset =>
            {
                fixedAsset.UpdateStatus();
            });

            erpNodeDBContext.SaveChanges();
        }

        public IQueryable<DeprecateSchedule> GetDeprecateSchedule(DateTime date)
        {
            return erpNodeDBContext.DeprecateSchedules
               .Where(d => d.EndDate <= date)
               .GroupBy(s => s.FixedAssetId)
               .Select(s => s.OrderByDescending(x => x.EndDate).FirstOrDefault());
        }
        public IQueryable<Models.Accounting.FiscalYear> FiscalYearChoice() => erpNodeDBContext.DeprecateSchedules
               .GroupBy(s => s.FiscalYear)
               .Select(s => s.Key);
        public void CreateDeprecationSchedule()
        {
            Console.WriteLine("Create Depreciation Schedule");

            var fiscalYear = organization.FiscalYears.CurrentPeriod;

            erpNodeDBContext.FixedAssets.OrderBy(ass => ass.StartDeprecationDate).ToList()
                .ForEach(fixedAsset =>
                {
                    fixedAsset.CreateDepreciationSchedule();
                    erpNodeDBContext.SaveChanges();
                    // Console.WriteLine(" > Save Complete");
                });
            Console.WriteLine(" > Depreciation Schedule Complete");
            erpNodeDBContext.SaveChanges();
            Console.WriteLine(" > DB Save Complete");
            this.RemoveNullSchedule();
            Console.WriteLine("Remove Null Schedule Complete");
            this.UpdateFiscal();
            Console.WriteLine("Update Fiscal Complete");
        }
        public void UpdateFiscal()
        {
            erpNodeDBContext.DeprecateSchedules
                .Where(d => d.FiscalYearId == null)
                .ToList()
                .ForEach(d => d.FiscalYear = organization.FiscalYears.Find(d.EndDate));
            erpNodeDBContext.SaveChanges();
        }
        public void ReOrder()
        {
            var assetGroups = erpNodeDBContext.FixedAssets.ToList()
                .GroupBy(a => a.StartDeprecationDate.Year)
                .Select(go => new
                {
                    year = go.Key,
                    Assets = go.OrderBy(a => a.StartDeprecationDate).ToList(),
                })
                .ToList();


            assetGroups.ForEach(g =>
            {
                int i = 0;
                g.Assets.ForEach(a =>
                {
                    a.No = ++i;
                    a.FiscalYear = organization.FiscalYears.Find(a.StartDeprecationDate);

                });

            });

            erpNodeDBContext.SaveChanges();
        }
        public void RemoveNullSchedule()
        {
            var nullSchedules = erpNodeDBContext.DeprecateSchedules
                .Where(s => s.FixedAssetId == null);

            erpNodeDBContext.DeprecateSchedules.RemoveRange(nullSchedules);
            erpNodeDBContext.SaveChanges();
        }
        public void SyncTransaction()
        {
            var fixedAssetsTransactionItems = erpNodeDBContext
                .CommercialItems
                .Where(ti => ti.Item.ItemType == Models.Items.Enums.ItemTypes.Asset)
                .Where(ti => ti.Transaction.Status == TransactionStatus.Open)
                .ToList();

            foreach (var fixedAssetsTransactionItem in fixedAssetsTransactionItems)
            {
                Models.Assets.Asset fixedAsset = new Asset()
                {
                    Id = fixedAssetsTransactionItem.Id,
                    AssetValue = fixedAssetsTransactionItem.LineTotal,
                    Name = fixedAssetsTransactionItem.Item.PartNumber
                };

                erpNodeDBContext.FixedAssets.Add(fixedAsset);
            }
            erpNodeDBContext.SaveChanges();
        }
        public void UnPost()
        {
            Console.WriteLine("> Un Post" + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            erpNodeDBContext.FixedAssets.ToList()
                .ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }
        public List<Asset> ReadyForPost
        {
            get
            {
                var firstDate = organization.DataItems.FirstDate;
                return erpNodeDBContext.FixedAssets
                    .Where(s => s.PostStatus == LedgerPostStatus.Editable && s.StartDeprecationDate >= firstDate)
                    .OrderBy(s => s.StartDeprecationDate).ToList();
            }
        }
        public void PostLedger()
        {
            var postingTransactions = this.ReadyForPost;
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
        public void UnPost(Asset fixedAsset)
        {
            organization.LedgersDal.RemoveTransaction(fixedAsset.Id);
            fixedAsset.PostStatus = LedgerPostStatus.Editable;


            fixedAsset.DepreciationSchedules.ToList().ForEach(ds =>
            {
                organization.LedgersDal.RemoveTransaction(ds.Id);
                ds.PostStatus = LedgerPostStatus.Editable;
            });

            erpNodeDBContext.SaveChanges();
        }
        public bool PostLedger(Asset tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.StartDeprecationDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };


            trLedger.Credit(tr.FixedAssetType.AssetAccount, tr.AssetValue);
            trLedger.Debit(tr.FixedAssetType.AwaitDeprecateAccount, tr.AssetValue);


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