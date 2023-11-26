
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Transactions.Commercials;
using KeeperCore.ERPNode.Models.Transactions;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models.Logging;

namespace KeeperCore.ERPNode.DAL.Transactions
{
    public class QueryDateRange
    {
        public QueryDateRange(DateTime starTime, DateTime endDate)
        {
            StarTime = starTime;
            EndDate = endDate;
        }

        public DateTime StarTime { get; private set; }
        public DateTime EndDate { get; private set; }
    }

    public class Sales : Commercials
    {
        public Sales(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.Sale;
        }
        public IQueryable<Sale> GetQueryable()
        {
            return erpNodeDBContext.Sales;
        }
        public new Sale Find(Guid transactionId) => erpNodeDBContext.Sales.Find(transactionId);


        public IQueryable<Sale> GetByStatus(CommercialStatus status)
             => this.GetQueryable()
                    .Where(t => t.Status == status)
                    .Include(t => t.Profile);
        public IQueryable<Sale> GetByViewStatus(CommercialViewStatus status)
        {
            switch (status)
            {
                case CommercialViewStatus.Open:
                    return this.GetQueryable().Where(t => t.Status == CommercialStatus.Open).Include(t => t.Profile);
                case CommercialViewStatus.Paid:
                    return this.GetQueryable().Where(t => t.Status == CommercialStatus.Paid).Include(t => t.Profile);
                case CommercialViewStatus.OverDue:
                    return this.GetOverDueList();
                case CommercialViewStatus.LastFiscal:
                    return this.GetLastFiscal();
                case CommercialViewStatus.All:
                default:
                    return this.GetQueryable();
            }

        }
        public Sale Search(string group, string number)
        {
            var searchDocument = $"{group}-{number}";
            return erpNodeDBContext.Sales
                 .Where(s => s.PartialName == searchDocument || s.Reference == searchDocument)
                 .FirstOrDefault();
        }
        public IQueryable<Sale> GetLastFiscal()
        {
            var currentFiscal = organization.FiscalYears.CurrentPeriod;

            var burchaseBeforeDate = currentFiscal.EndDate;
            var salesList = erpNodeDBContext.Sales
                .Where(b => b.Total > 0)
                .Where(b => b.TrnDate < currentFiscal.StartDate)
                .Where(b => b.CloseDate >= currentFiscal.StartDate || b.CloseDate == null);
            return salesList;
        }
        public IQueryable<Sale> GetOverDueList()
        {
            var expDate = DateTime.Now.AddDays(-30);
            var overdueList = erpNodeDBContext.Sales
                .Where(b => b.Status == CommercialStatus.Open)
                .Where(b => b.TrnDate < expDate);
            return overdueList;
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
        public Reports.Customers.Sale GetExportReport(Guid id, string documentType)
        {
            var transactions = this.FindList(id);

            var Report = new ERPNode.Reports.Customers.Sale()
            {
                DataSource = transactions,
                Name = id.ToString("D")
            };

            Report.Parameters["DocumentType"].Value = documentType ?? transactions.FirstOrDefault().DocumentTypeName;
            return Report;
        }




        public new Sale Find(int transactionNo) => erpNodeDBContext.Sales.Where(t => t.No == transactionNo).FirstOrDefault();
        public int OpenCount => erpNodeDBContext.Sales.Where(b => b.Status == CommercialStatus.Open).Count();
        public int OverDueCount
        {
            get
            {
                var expDate = DateTime.Now.AddDays(-30);
                var overdueCount = erpNodeDBContext.Sales
                    .Where(b => b.Status == CommercialStatus.Open)
                    .Where(b => b.TrnDate < expDate)
                    .Count();
                return overdueCount;
            }
        }
        public Decimal OpenBalance => erpNodeDBContext.Sales
            .Where(b => b.Status == CommercialStatus.Open)
            .Sum(b => (Decimal?)b.Total) ?? 0;
        public Decimal OverDueBalance
        {
            get
            {
                var expDate = DateTime.Now.AddDays(-30);
                var overdueBalance = erpNodeDBContext.Sales
                    .Where(b => b.Status == CommercialStatus.Open)
                    .Where(b => b.TrnDate < expDate)
                    .Sum(b => (Decimal?)b.Total) ?? 0;
                return overdueBalance;
            }
        }

        public List<Sale> GetReadyForPost()
        {
            return erpNodeDBContext.Sales
                .Where(s => s.PostStatus == LedgerPostStatus.Editable && s.Status != CommercialStatus.Void)
                .Include(s => s.CommercialItems).ToList();
        }
        public List<Sale> GetReceiveAble(DateTime viewDate)
        {
            var openSales = erpNodeDBContext.Sales
                .Where(i => i.TrnDate <= viewDate)
                .ToList();

            return openSales.Where(i => i.CloseDate == null || i.CloseDate > viewDate).ToList();

        }
        public int GetNextNumber() => (erpNodeDBContext.Sales.Max(e => (int?)e.No) ?? 0) + 1;
        public int NextMonthlyNo(DateTime trDate)
        {
            return (erpNodeDBContext.Sales
                .Where(s => s.TrnDate.Month == trDate.Month && s.TrnDate.Year == trDate.Year)
                .Max(e => (int?)e.NoInMonth) ?? 0) + 1;
        }

        [NotMapped]
        public IQueryable<Sale> PaidTransactions => erpNodeDBContext.Sales.Where(s => s.Status == CommercialStatus.Paid);

        public Sale Create(Models.Profiles.Profile profile, DateTime trDate, Guid? responsibleId = null)
        {

            var newSale = new Sale()
            {
                No = this.GetNextNumber(),
                NoInMonth = this.NextMonthlyNo(trDate),
                ProfileId = profile.Id,
                Profile = profile,
                TrnDate = trDate,
                Status = CommercialStatus.Open,
                CommercialItems = new HashSet<CommercialItem>(),
                TransactionType = this.transactionType,
                ResponsibleMemberId = responsibleId
            };
            newSale.TaxCode = organization.TaxCodes.GetDefaultOuput;
            newSale.UpdateName();

            erpNodeDBContext.Sales.Add(newSale);
            erpNodeDBContext.SaveChanges();

            return newSale;
        }

        public List<Commercial> FindList(Guid id) => this.erpNodeDBContext.Commercials.Where(tr => tr.Id == id).Take(1).ToList();

        public Sale UpdateChanges(Sale sale)
        {
            var existSale = erpNodeDBContext.Sales.Find(sale.Id);

            if (existSale == null)
                throw new Exception("Update fail, transaction not found");
            else if (existSale.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Update fail, transaction aleardy posted");
            else
            {
                existSale.Reference = sale.Reference;
                existSale.Memo = sale.Memo;
                existSale.ProjectId = sale.ProjectId;
                existSale.TrnDate = sale.TrnDate;
                existSale.PaymentTermId = sale.PaymentTermId;
                existSale.ShippingTermId = sale.ShippingTermId;



                existSale.TaxCodeId = sale.TaxCodeId;
                existSale.TaxCode = organization.TaxCodes.Find(sale.TaxCodeId);

                existSale.UpdateName();
                erpNodeDBContext.SaveChanges();

                existSale.ReCalculate();
                erpNodeDBContext.SaveChanges();
                return sale;
            }
        }
        public List<Sale> ListByProfile(Guid id) => organization.Sales.GetQueryable()
                .Where(Transaction => Transaction.ProfileId == id)
                .ToList();
        public void UpdateDailyBalance()
        {
            this.ClearDailyBalance();
            var dailySales = this.GetQueryable().GroupBy(t => new { t.TrnDate })
                                           .Select(go => new
                                           {
                                               IncomeAmount = go.Sum(i => i.Total),
                                               TrnDate = go.Key.TrnDate
                                           }).ToList();

            Console.WriteLine("> {0} Update Daily Sales Balance {2} [{1}]", DateTime.Now.ToLongTimeString(), dailySales.Count(), this.transactionType.ToString());

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;

            dailySales.ForEach(ds =>
            {
                var dailySale = new CommercialDailyBalance()
                {
                    TrnDate = ds.TrnDate,
                    Balance = ds.IncomeAmount,
                    Type = this.transactionType,
                    Id = Guid.NewGuid()
                };
                this.erpNodeDBContext.CommercialDailyBalances.Add(dailySale);
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
        public void ReOrder()
        {
            int trackNo = 1;
            var Sales = erpNodeDBContext.Sales
                .OrderBy(tr => tr.TrnDate)
                .GroupBy(o => new { o.TrnDate.Year, o.TrnDate.Month })
                .Select(go => new
                {
                    year = go.Key.Year,
                    month = go.Key.Month,
                    sales = go.ToList()
                })
                .OrderBy(s => s.year).ThenBy(s => s.month)
                .ToList();

            foreach (var saleMonth in Sales)
            {
                int i = 1;
                saleMonth.sales
                    .OrderBy(p => p.NoInMonth)
                    .ToList()
                    .ForEach(p =>
                {
                    Console.WriteLine($"{p.NoInMonth} => {i}");
                    p.NoInMonth = i++;
                    p.No = trackNo++;
                    p.UpdateName();
                });
                Console.WriteLine($"-----------{saleMonth.year}/{saleMonth.month} -------------");

            }
            erpNodeDBContext.SaveChanges();
        }

        public void Post()
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
                    this.Post(s, false);
                });
            }

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        private bool Post(Sale sale, bool saveChange = true)
        {
            if (sale.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Post fail, Transaction aleardy posted");
            if (sale.Total == 0)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = sale.Id,
                TrnDate = sale.TrnDate,
                TransactionName = sale.Name,
                ProfileName = sale.Profile.Name,
                TransactionNo = sale.No,
                TransactionType = transactionType,
                Reference = sale.Reference,
            };

            this.PostItems(trLedger, sale);

            if (sale.TaxCode != null)
                trLedger.Credit(sale.TaxCode.TaxAccount, sale.Tax);

            trLedger.Debit(organization.SystemAccounts.AccountReceivable, sale.Total);

            if (trLedger.FinalValidate())
            {
                sale.PostStatus = LedgerPostStatus.Locked;
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
            }
            else
            {
                trLedger.RemoveLedgers();
                organization.EventLogs.NewEventLog(EventLogLevel.Error, "1011", "Error Posting", trLedger.TransactionName, "");
                return false;
            }

            if (saveChange)
                erpNodeDBContext.SaveChanges();

            return true;
        }
        private void PostItems(Models.Accounting.TransactionLedger trLedger, Sale sale)
        {
            foreach (var transactionItem in sale.CommercialItems)
            {
                string memo = transactionItem.Amount.ToString() + " x " + transactionItem.ItemPartNumber;
                trLedger.Credit(transactionItem.Item.IncomeAccount, transactionItem.LineTotal, memo);
            }
        }
        public bool Delete(Sale sale)
        {
            if (sale == null)
                throw new Exception("Delete fail, Transaction not found");
            else if (sale.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Delete fail, Transaaction aleardy posted");
            else
            {
                sale.RemoveItems();
                erpNodeDBContext.Commercials.Remove(sale);
                organization.SaveChanges();
                return true;
            }
        }
        public void UnPost(Sale sale)
        {
            if (sale.PostStatus == LedgerPostStatus.Editable)
                throw new Exception("Post fail, Transaction is not posted");
            else if (sale.PostStatus == LedgerPostStatus.PreOpening)
                throw new Exception("Post fail, Transaction is pre fiscal year.");
            else
            {
                Console.WriteLine("> Un Posting, " + sale.Name + "," + sale.Status);
                organization.LedgersDal.RemoveTransaction(sale.Id);
                sale.PostStatus = LedgerPostStatus.Editable;
                this.SaveChanges();
            }
        }
        public void Unpost()
        {
            organization.LedgersDal.UnPost(this.transactionType);

            var sqlCommand = "UPDATE [dbo].[ERP_Transactions_Commercial] SET  [PostStatus] = '0' WHERE  [Discriminator] ='{0}' ";
            sqlCommand = string.Format(sqlCommand, this.transactionType.ToString());
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

        }
    }
}
