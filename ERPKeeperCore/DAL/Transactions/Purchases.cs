using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Transactions.Enums;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial.Payments;
using KeeperCore.ERPNode.Models.Financial.Payments.Enums;
using KeeperCore.ERPNode.Models.Logging;


namespace KeeperCore.ERPNode.DAL.Transactions
{
    public class Purchases : Commercials
    {
        public Purchases(Organization organization) : base(organization)
        {
        
        }

        public IQueryable<Purchase> GetQueryable()
        {
            return erpNodeDBContext.Purchases;
        }
        public IQueryable<Purchase> GetByStatus(TransactionStatus status) => this.GetQueryable()
            .Where(t => t.Status == status)
            .Include(t => t.Profile);
        public List<Purchase> GetReadyForPost()
        {
            return this.erpNodeDBContext
                .Purchases
                .Where(s => s.PostStatus == LedgerPostStatus.Editable && s.Status != TransactionStatus.Void)
                .Include(s => s.CommercialItems)
                .ToList();
        }

        public int OpenCount => this.erpNodeDBContext
            .Purchases
            .Where(b => b.Status == TransactionStatus.Open)
            .Count();
        public int PaidCount => this.erpNodeDBContext
            .Purchases
            .Where(b => b.Status == TransactionStatus.Paid)
            .Count();
        public int OverDueCount
        {
            get
            {
                var expDate = DateTime.Now.AddDays(-30);
                var overdueCount = erpNodeDBContext.Purchases
                    .Where(b => b.Status == TransactionStatus.Open)
                    .Where(b => b.TrnDate < expDate)
                    .Count();
                return overdueCount;
            }
        }

        public Decimal OpenBalance => erpNodeDBContext.Purchases
            .Where(b => b.Status == TransactionStatus.Open)
            .Sum(b => (Decimal?)b.Total) ?? 0;
        public Decimal PaidBalance => erpNodeDBContext.Purchases
    .Where(b => b.Status == TransactionStatus.Paid)
    .Sum(b => (Decimal?)b.Total) ?? 0;
        public Decimal AllBalance => erpNodeDBContext.Purchases
    .Sum(b => (Decimal?)b.Total) ?? 0;

        public IQueryable<Purchase> GetByViewStatus(CommercialViewStatus status)
        {
            switch (status)
            {
                case CommercialViewStatus.Open:
                    return this.GetQueryable().Where(t => t.Status == TransactionStatus.Open).Include(t => t.Profile);
                case CommercialViewStatus.Paid:
                    return this.GetQueryable().Where(t => t.Status == TransactionStatus.Paid).Include(t => t.Profile);
                case CommercialViewStatus.OverDue:
                    return this.GetOverDueList();
                case CommercialViewStatus.LastFiscal:
                    return this.GetLastFiscal();
                case CommercialViewStatus.All:
                default:
                    return this.GetQueryable();
            }

        }
        public IQueryable<Purchase> GetLastFiscal()
        {
            var currentFiscal = organization.FiscalYears.CurrentPeriod;

            var burchaseBeforeDate = currentFiscal.EndDate;
            var PurchasesList = erpNodeDBContext.Purchases
                .Where(b => b.TrnDate < currentFiscal.StartDate)
                .Where(b => b.CloseDate >= currentFiscal.StartDate);
            return PurchasesList;
        }
        public IQueryable<Purchase> GetOverDueList()
        {
            var expDate = DateTime.Now.AddDays(-30);
            var overdueList = erpNodeDBContext.Purchases
                .Where(b => b.Status == TransactionStatus.Open)
                .Where(b => b.TrnDate < expDate);
            return overdueList;
        }


        public Decimal OverDueBalance
        {
            get
            {
                var expDate = DateTime.Now.AddDays(-30);
                var overdueBalance = erpNodeDBContext.Purchases
                    .Where(b => b.Status == TransactionStatus.Open)
                    .Where(b => b.TrnDate < expDate)
                    .Sum(b => (Decimal?)b.Total) ?? 0;
                return overdueBalance;
            }
        }

        public List<CommercialDailyBalance> ListDailyBalances(int duration)
        {
            DateTime StartDate = DateTime.Now.AddDays(duration * -1);

            var dailyBalances = this.erpNodeDBContext.CommercialDailyBalances
                 .Where(db => db.TrnDate > StartDate && db.Type == this.transactionType)
                 .ToList();


            var firstDateBalance = dailyBalances.Where(b => b.TrnDate == StartDate).FirstOrDefault();

            if (firstDateBalance == null)
            {
                firstDateBalance = new CommercialDailyBalance()
                {
                    TrnDate = StartDate,
                    Balance = 0,
                    Type = this.transactionType,
                    Id = Guid.NewGuid()
                };

                dailyBalances.Add(firstDateBalance);
            }


            return dailyBalances;
        }

        public void UpdatePurchasingBalance()
        {
            var BalanceTables = erpNodeDBContext.Purchases
                .GroupBy(o => o.ProfileId)
                .ToList()
                .Select(go => new
                {
                    Profile = go.Select(i => i.Profile).FirstOrDefault(),
                    TotalPurchase = go.Sum(i => i.Total),
                    CountPurchase = go.Count(),
                    TotalBalance = go.Sum(i => i.TotalBalance),
                    CountBalance = go.Where(i => i.TotalBalance > 0).Count(),
                })
                .ToList();


            erpNodeDBContext.Suppliers.ToList().ForEach(p =>
            {
                p.SumPurchaseBalance = 0;
                p.TotalBalance = 0;
                p.CountPurchases = 0;
            });

            BalanceTables.ForEach(b =>
            {
                if (b.Profile != null && b.Profile.Supplier != null)
                {
                    b.Profile.Supplier.SumPurchaseBalance = b.TotalPurchase;
                    b.Profile.Supplier.CountPurchases = b.CountPurchase;
                    b.Profile.Supplier.TotalBalance = b.TotalBalance;
                    b.Profile.Supplier.CountBalance = b.CountBalance;
                }
                else
                {
                    organization.EventLogs.NewEventLog(EventLogLevel.Error, "1111", "Unknow b.profile", b.Profile.Name, "");
                }
            });

            erpNodeDBContext.SaveChanges();
        }
        public void AddPayment(Guid id, DateTime payDate)
        {
            var purchase = erpNodeDBContext.Purchases.Find(id);

            var payment = new SupplierPayment()
            {
                Id = Guid.NewGuid(),
                TrnDate = payDate,
                AssetAccount = organization.SystemAccounts.Cash,
                LiabilityAccount = organization.SystemAccounts.AccountPayable,
            };


            purchase.UpdatePaymentStatus();
            erpNodeDBContext.SaveChanges();

        }

        public new Purchase Find(Guid transactionId) => erpNodeDBContext.Purchases.Find(transactionId);

        public int GetNextNumber()
        {
            try
            {
                return erpNodeDBContext.Purchases.Max(e => e.No) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public int GetNextMonthlyNo(DateTime trDate)
        {
            return (erpNodeDBContext.Purchases
                .Where(s => s.TrnDate.Month == trDate.Month && s.TrnDate.Year == trDate.Year)
                .Max(e => (int?)e.NoInMonth) ?? 0) + 1;
        }
        public void ReOrder()
        {
            int trackNo = 1;

            var Purchases = erpNodeDBContext.Purchases
                .GroupBy(o => new { o.TrnDate.Year, o.TrnDate.Month })
                .Select(go => new
                {
                    year = go.Key.Year,
                    month = go.Key.Month,
                    purchases = go.ToList()
                })
                .OrderBy(s => s.year).ThenBy(s => s.month)
                .ToList();

            foreach (var purchaseMonth in Purchases)
            {
                int i = 0;
                purchaseMonth.purchases
                    .OrderBy(p => p.NoInMonth)
                    .ToList()
                    .ForEach(p =>
                  {
                      p.NoInMonth = ++i;
                      p.No = trackNo++;
                      p.UpdateName();
                  });
            }
            this.SaveChanges();
        }

        public Purchase Update(Purchase purchase)
        {
            var exPurchase = erpNodeDBContext.Purchases.Find(purchase.Id);

            if (exPurchase == null)
                throw new Exception("Cannot update, Transaction not found");
            else if (exPurchase.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Cannot update, Transaction is Posted");
            else
            {

                exPurchase.NoInMonth = purchase.NoInMonth;
                exPurchase.ProjectId = purchase.ProjectId;
                exPurchase.TrnDate = purchase.TrnDate;
                exPurchase.Reference = purchase.Reference;
                exPurchase.Memo = purchase.Memo;
                exPurchase.PaymentTermId = purchase.PaymentTermId;
                exPurchase.ShippingTermId = purchase.ShippingTermId;

                exPurchase.TaxCodeId = purchase.TaxCodeId;
                exPurchase.TaxCode = organization.TaxCodes.Find(exPurchase.TaxCodeId);

                exPurchase.TaxOffset = purchase.TaxOffset;
                exPurchase.UpdateName();
                exPurchase.ReCalculate();
                erpNodeDBContext.SaveChanges();
            }


            return exPurchase;
        }
        public void UpdateProfilesCache()
        {
            var commercials = erpNodeDBContext.Commercials.ToList();

            commercials.Where(c => c.ProfileId != null).ToList().ForEach(c =>
            {
                c.CacheProfileName = c.Profile.DisplayName;
            });
            erpNodeDBContext.SaveChanges();
        }
        public Purchase Create(Guid profileId, DateTime trDate)
        {
            var profile = organization.Profiles.Find(profileId);

            var newPurchase = new Purchase()
            {
                ProfileId = profileId,
                No = this.GetNextNumber(),
                NoInMonth = this.GetNextMonthlyNo(trDate),
                TrnDate = trDate,
                ProfileName = profile.Name,
                TransactionType = this.transactionType
            };

            newPurchase.UpdateName();
            erpNodeDBContext.Commercials.Add(newPurchase);

            newPurchase.ReCalculate();

            erpNodeDBContext.SaveChanges();

            return newPurchase;
        }

        public bool Delete(Purchase purchase)
        {
            if (purchase == null)
                throw new Exception("Delete fail, Transaction not found");
            else if (purchase.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Delete fail, Transaaction aleardy posted");
            else
            {
                purchase.RemoveItems();
                erpNodeDBContext.Commercials.Remove(purchase);
                organization.SaveChanges();
            }
            return true;
        }

        public Purchase Copy(Purchase originalPurchase, DateTime trDate)
        {
            var clonePurchase = this.erpNodeDBContext.Purchases
                    .AsNoTracking()
                    .Include(p => p.CommercialItems)
                    .FirstOrDefault(x => x.Id == originalPurchase.Id);

            if (clonePurchase == null)
                throw new Exception("Copy Fail, Transaction not found");
            else
            {
                clonePurchase.Id = Guid.NewGuid();
                clonePurchase.TrnDate = trDate;
                clonePurchase.Reference = "Clone-" + clonePurchase.Reference;
                clonePurchase.No = organization.Purchases.GetNextNumber();
                clonePurchase.PostStatus = LedgerPostStatus.Editable;

                clonePurchase.CommercialItems.ToList().ForEach(ci => ci.Id = Guid.NewGuid());

                this.erpNodeDBContext.Purchases.Add(clonePurchase);
                this.erpNodeDBContext.SaveChanges();

                return clonePurchase;
            }

        }
        public void UpdateDailyBalance()
        {
            this.ClearDailyBalance();
            var dailyPurchases = this.GetQueryable().GroupBy(t => new { t.TrnDate })
                                           .Select(go => new
                                           {
                                               IncomeAmount = go.Sum(i => i.Total),
                                               TrnDate = go.Key.TrnDate
                                           }).ToList();

            Console.WriteLine("> {0} Update Daily Purchases Balance {2} [{1}]", DateTime.Now.ToLongTimeString(), dailyPurchases.Count(), this.transactionType.ToString());

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;

            dailyPurchases.ForEach(ds =>
            {
                var dailyPurchase = new CommercialDailyBalance()
                {
                    TrnDate = ds.TrnDate,
                    Balance = ds.IncomeAmount,
                    Type = this.transactionType,
                    Id = Guid.NewGuid()
                };
                this.erpNodeDBContext.CommercialDailyBalances.Add(dailyPurchase);
            });

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        public void ClearDailyBalance()
        {
            var removeList = this.erpNodeDBContext.CommercialDailyBalances.Where(t => t.Type == this.transactionType).ToList();
            this.erpNodeDBContext.CommercialDailyBalances.RemoveRange(removeList);
            this.SaveChanges();
        }

        public void PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();

            string logTitle = string.Format("> Post {0} [{1}]", this.trString, postingTransactions.Count());
            organization.EventLogs.NewEventLog(EventLogLevel.Information, "00", logTitle, null, "");

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;

            using (var progress = new Helpers.ProgressBar())
            {
                var currentIndex = 0;

                postingTransactions.ForEach(s =>
                {
                    progress.Report(currentIndex++, postingTransactions.Count);
                    this.PostLedger(s, false);
                });
            }

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        public bool PostLedger(Purchase tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Post fail, Transaaction aleardy posted");
            if (tr.Total == 0)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType,
                Reference = tr.Reference,
                ProfileName = tr.Profile?.Name,
            };

            this.PostLedger_Items(trLedger, tr);

            if (tr.TaxCode != null)
                trLedger.Debit(tr.TaxCode.TaxAccount, tr.Tax);
            trLedger.Credit(organization.SystemAccounts.AccountPayable, tr.Total);

            if (trLedger.FinalValidate())
            {
                tr.PostStatus = LedgerPostStatus.Locked;
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
            }
            else
            {
                trLedger.RemoveLedgers();
                organization.EventLogs.NewEventLog(EventLogLevel.Error,
                    "1011", "Error Posting", trLedger.TransactionName, "");
                return false;
            }

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
        }

        private void PostLedger_Items(Models.Accounting.TransactionLedger trLedger, Purchase purchase)
        {
            foreach (var transactionItem in purchase.CommercialItems)
            {
                string memo = transactionItem.Amount.ToString() + " x " + transactionItem.ItemPartNumber;

                if (transactionItem.Item.GetPurchaseAccount == null)
                    transactionItem.Item.PurchaseAccount = organization.SystemAccounts.COSG;

                trLedger.Debit(transactionItem.Item.GetPurchaseAccount, transactionItem.LineTotal, memo);

            }
        }


        public void UnPost(Purchase purchase)
        {
            Console.WriteLine("> Un Posting ,PO" + purchase.No + "," + purchase.Status);
            organization.LedgersDal.RemoveTransaction(purchase.Id);

            purchase.PostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
        }
        public void UnPost()
        {
            organization.LedgersDal.UnPost(this.transactionType);

            var sqlCommand = "UPDATE [dbo].[ERP_Transactions_Commercial] SET  [PostStatus] = '0' WHERE  [Discriminator] ='{0}' ";
            sqlCommand = string.Format(sqlCommand, this.transactionType.ToString());
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
        }

    }
}