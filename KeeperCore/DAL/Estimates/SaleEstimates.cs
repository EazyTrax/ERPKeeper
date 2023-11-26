using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Estimations;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using KeeperCore.ERPNode.Models.Estimations.Enums;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.DAL.Estimates
{
    public class SaleEstimates : ERPNodeDalRepository
    {
        public SaleEstimates(Organization organization) : base(organization)
        {

        }

        public List<SaleQuote> GetAll()
        {
            return erpNodeDBContext.SaleEstimates.ToList();
        }

        public SaleQuote Find(Guid quoteId)
        {
            return erpNodeDBContext.SaleEstimates.Find(quoteId);
        }

        public void Remove(SaleQuote entity)
        {

        }

        public List<SaleQuote> ListByStatus(QuoteStatus? status)
        {
            if (status == null)
                return erpNodeDBContext.SaleEstimates.Where(e => e.Status != QuoteStatus.Void && e.Status != QuoteStatus.Close)
                .ToList();
            else
                return erpNodeDBContext.SaleEstimates.Where(e => e.Status == status)
              .ToList();

        }

        public int NextNumber => (erpNodeDBContext.SaleEstimates.Max(b => (int?)b.No) ?? 0) + 1;

        public IQueryable<SaleQuote> Query() => erpNodeDBContext.SaleEstimates;

        public void VoidExpired()
        {
            DateTime lastestDate = DateTime.Now.AddDays(-90);

            var estimates = erpNodeDBContext.SaleEstimates.Where(t => t.TrnDate < lastestDate)
              .Where(t => t.Status == QuoteStatus.Quote)
              .ToList();

            foreach (var est in estimates)
            {
                est.Status = QuoteStatus.Void;
            }

            erpNodeDBContext.SaveChanges();
        }




        public SaleQuote Create(Models.Profiles.Profile profile, DateTime createDate, Guid? responsibleUserId = null)
        {
            var newQuote = new SaleQuote()
            {
                TransactionType = ERPObjectType.SaleQuote,
                Id = Guid.NewGuid(),
                No = this.NextNumber,
                ProfileId = profile.Id,
                TrnDate = createDate,
                TaxCode = organization.TaxCodes.GetDefaultOuput,
                ExpiredInDayCount = 90,
                UserId = responsibleUserId
            };

            erpNodeDBContext.SaleEstimates.Add(newQuote);
            erpNodeDBContext.SaveChanges();

            return newQuote;
        }

        public void ReOrder()
        {
            var transfers = erpNodeDBContext.SaleEstimates
              .OrderBy(t => t.TrnDate).ThenBy(t => t.No)
              .ToList();

            int i = 0;
            foreach (var transfer in transfers)
            {
                transfer.No = ++i;
            }

            erpNodeDBContext.SaveChanges();
        }

        public SaleQuote UpdateChanges(SaleQuote estimate)
        {
            var existQuote = erpNodeDBContext.SaleEstimates.Find(estimate.Id);
            if (existQuote != null)
            {
                existQuote.Reference = estimate.Reference;
                existQuote.Memo = estimate.Memo;

                existQuote.ProjectId = estimate.ProjectId;
                existQuote.TrnDate = estimate.TrnDate;
                existQuote.ExpiredInDayCount = estimate.ExpiredInDayCount;


                existQuote.PaymentTermId = estimate.PaymentTermId;
                existQuote.ShippingTermId = estimate.ShippingTermId;

                existQuote.TaxCodeId = estimate.TaxCodeId;
                existQuote.TaxCode = organization.TaxCodes.Find(estimate.TaxCodeId);


                existQuote.UpdateName();
                existQuote.Calculate();

                erpNodeDBContext.SaveChanges();
                return estimate;
            }

            return null;
        }

        public void Delete(Guid id)
        {
            var salesQuote = organization.SaleEstimates.Find(id);

            foreach (var item in salesQuote.Items.ToList())
            {
                salesQuote.Items.Remove(item);
            }
            organization.SaveChanges();


            erpNodeDBContext.SaleEstimates.Remove(salesQuote);
            organization.SaveChanges();
        }


        public SaleQuote Copy(SaleQuote originalSaleQuote, DateTime trDate)
        {
            var cloneSaleQuote = this.erpNodeDBContext.SaleEstimates
                    .AsNoTracking()
                    .Include(p => p.Items)
                    .FirstOrDefault(x => x.Id == originalSaleQuote.Id);

            cloneSaleQuote.Id = Guid.NewGuid();
            cloneSaleQuote.TrnDate = trDate;
            cloneSaleQuote.Reference = "Clone-" + cloneSaleQuote.Reference;
            cloneSaleQuote.No = organization.SaleEstimates.NextNumber;
            cloneSaleQuote.Status = QuoteStatus.Quote;
            cloneSaleQuote.Items.ToList().ForEach(ci => ci.Id = Guid.NewGuid());


            this.erpNodeDBContext.SaleEstimates.Add(cloneSaleQuote);
            this.erpNodeDBContext.SaveChanges();

            return cloneSaleQuote;
        }



        public List<SaleQuote> ListByProfile(Guid id) => organization.SaleEstimates.Query()
                .Where(Transaction => Transaction.ProfileId == id)
                .ToList();

        internal void UpdateDoumentsName()
        {
            erpNodeDBContext.SaleEstimates.ToList().ForEach(c =>
            {
                Console.Write("#");
                c.UpdateName();
            });
            erpNodeDBContext.SaveChanges();
        }

        public Reports.Customers.Quote Export(Guid id, string documentType, string contactName)
        {
            var salesQuotes = this.Query().Where(tr => tr.Id == id).ToList();

            var Report = new ERPNode.Reports.Customers.Quote()
            {
                DataSource = salesQuotes,
                Name = "SE-" + salesQuotes.FirstOrDefault().No.ToString()
            };

            Report.Parameters["DocumentType"].Value = documentType;
            Report.Parameters["Contact"].Value = contactName;
            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["Contact"].Visible = false;
            return Report;
        }
    }
}