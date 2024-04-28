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

namespace ERPKeeper.Node.DAL.Assets
{
    public class FixedAssets : ERPNodeDalRepository
    {
        public FixedAssets(Organization organization) : base(organization)
        {
            transactionType = Models.Accounting.Enums.ERPObjectType.FixedAsset;
        }

        public IQueryable<FixedAsset> Query() => erpNodeDBContext.Assets;

        public List<FixedAsset> GetListAll() => erpNodeDBContext.Assets.OrderBy(a => a.StartDeprecationDate).ToList();


        public FixedAsset Find(Guid uid) => erpNodeDBContext.Assets.Find(uid);
        public FixedAsset Save(FixedAsset fixedAsset)
        {
            var exFixedAsset = erpNodeDBContext.Assets.Find(fixedAsset.Uid);

            if (exFixedAsset != null)
            {
                if (exFixedAsset.PostStatus != LedgerPostStatus.Locked)
                {
                    exFixedAsset.FiscalYear = organization.FiscalYears.Find(fixedAsset.StartDeprecationDate);
                    exFixedAsset.Reference = fixedAsset.Reference;
                    exFixedAsset.Memo = fixedAsset.Memo;
                    exFixedAsset.Name = fixedAsset.Name;
                    exFixedAsset.Code = fixedAsset.Code;
                    exFixedAsset.FixedAssetTypeUid = fixedAsset.FixedAssetTypeUid;
                    exFixedAsset.StartDeprecationDate = fixedAsset.StartDeprecationDate;
                    exFixedAsset.AssetValue = fixedAsset.AssetValue;
                    exFixedAsset.SavageValue = fixedAsset.SavageValue;
                }


                if (exFixedAsset.RetirementPostStatus != LedgerPostStatus.Locked)
                {
                    exFixedAsset.RetirementDate = fixedAsset.RetirementDate;
                }


                erpNodeDBContext.SaveChanges();
                return exFixedAsset;
            }
            else
            {
                fixedAsset.FiscalYear = organization.FiscalYears.Find(fixedAsset.StartDeprecationDate);
                erpNodeDBContext.Assets.Add(fixedAsset);
                erpNodeDBContext.SaveChanges();

                return fixedAsset;
            }
        }

        public void Remove(FixedAsset depAsset)
        {
            erpNodeDBContext.Assets.Remove(depAsset);
            erpNodeDBContext.SaveChanges();
        }

        public List<FixedAsset> ListByStatus(FixedAssetStatus status) => erpNodeDBContext.Assets.Where(d => d.Status == status).ToList();

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
               .GroupBy(s => s.FixedAssetUid)
               .Select(s => s.OrderByDescending(x => x.EndDate).FirstOrDefault());
        }
        public IQueryable<Models.Accounting.FiscalYear> FiscalYearChoice() => erpNodeDBContext.DeprecateSchedules
               .GroupBy(s => s.FiscalYear)
               .Select(s => s.Key);
        public void CreateDeprecationSchedule()
        {
            Console.WriteLine("Create Depreciation Schedule");

            var fiscalYear = organization.FiscalYears.CurrentPeriod;

            erpNodeDBContext.Assets.OrderBy(ass => ass.StartDeprecationDate).ToList()
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
                .Where(d => d.FiscalYearUid == null)
                .ToList()
                .ForEach(d => d.FiscalYear = organization.FiscalYears.Find(d.EndDate));
            erpNodeDBContext.SaveChanges();
        }
        public void ReOrder()
        {
            var assetGroups = erpNodeDBContext.Assets.ToList()
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
                .Where(s => s.FixedAssetUid == null);

            erpNodeDBContext.DeprecateSchedules.RemoveRange(nullSchedules);
            erpNodeDBContext.SaveChanges();
        }
        public void SyncTransaction()
        {
            var fixedAssetsTransactionItems = erpNodeDBContext
                .CommercialItems
                .Where(ti => ti.Item.ItemType == Models.Items.Enums.ItemTypes.Asset)
                .Where(ti => ti.Commercial.Status == CommercialStatus.Open)
                .ToList();

            foreach (var fixedAssetsTransactionItem in fixedAssetsTransactionItems)
            {
                Models.Assets.FixedAsset fixedAsset = new FixedAsset()
                {
                    Uid = fixedAssetsTransactionItem.Uid,
                    AssetValue = fixedAssetsTransactionItem.LineTotal,
                    Name = fixedAssetsTransactionItem.Item.PartNumber
                };

                erpNodeDBContext.Assets.Add(fixedAsset);
            }
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

            erpNodeDBContext.Assets.ToList()
                .ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }
        public List<FixedAsset> ReadyForPost
        {
            get
            {
                var firstDate = organization.DataItems.FirstDate;
                return erpNodeDBContext.Assets
                    .Where(s => s.Status != FixedAssetStatus.Draft && s.PostStatus == LedgerPostStatus.Editable && s.StartDeprecationDate >= firstDate)
                    .OrderBy(s => s.StartDeprecationDate).ToList();
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
        public void UnPost(FixedAsset fixedAsset)
        {
            organization.LedgersDal.RemoveTransaction(fixedAsset.Uid);
            fixedAsset.PostStatus = LedgerPostStatus.Editable;


            fixedAsset.DepreciationSchedules.ToList().ForEach(ds =>
            {
                organization.LedgersDal.RemoveTransaction(ds.Uid);
                ds.PostStatus = LedgerPostStatus.Editable;
            });

            erpNodeDBContext.SaveChanges();
        }
        public bool PostLedger(FixedAsset tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Uid = tr.Uid,
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

            trLedger.Credit(tr.FixedAssetType.AwaitDeprecateAccount, tr.AssetValue);
            trLedger.Debit(tr.FixedAssetType.AccumulateDeprecateAcc, tr.AssetValue);

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