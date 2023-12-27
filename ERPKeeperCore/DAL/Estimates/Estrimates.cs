using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Estimations;
using KeeperCore.ERPNode.Models.Estimations.Enums;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KeeperCore.ERPNode.DAL.Quotes
{
    public class Estrimates : ERPNodeDalRepository
    {
        public Estrimates(Organization organization) : base(organization)
        {

        }

        public List<Quote> GetAll()
        {
            return erpNodeDBContext.Estimates.ToList();
        }

        public IQueryable<Quote> Query() => erpNodeDBContext.Estimates;
        public Quote Find(Guid quoteId) => erpNodeDBContext.Estimates.Find(quoteId);
        public void Remove(Quote entity) => erpNodeDBContext.Estimates.Remove(entity);
        public int NextNumber => (erpNodeDBContext.Estimates.Max(e => (int?)e.No) ?? 0) + 1;
        public List<Quote> ListByStatus(QuoteStatus? status)
        {
            if (status == null)
              return   erpNodeDBContext.Estimates.ToList();
            else
                return erpNodeDBContext.Estimates.Where(e => e.Status == status).ToList();
        }

        public Quote Create(Guid profileId, DateTime createDate)
        {
            var newPurchaseQuote = new Quote()
            {
                TransactionType = ERPObjectType.PurchaseQuote,
                Id = Guid.NewGuid(),
                No = this.NextNumber,
                ProfileId = profileId,
                TrnDate = createDate,
                TaxCode = organization.TaxCodes.GetDefaultInput,
                ExpiredInDayCount = 90,
            };

            erpNodeDBContext.Estimates.Add(newPurchaseQuote);
            erpNodeDBContext.SaveChanges();

            return newPurchaseQuote;
        }

        public void ReOrder()
        {
            var transfers = erpNodeDBContext.Estimates
              .OrderBy(t => t.TrnDate)
              .ThenBy(t => t.No)
              .ToList();

            int i = 0;
            transfers.ForEach(t => t.No = i++);

            erpNodeDBContext.SaveChanges();
        }

        public Quote SaveChanges(Quote est)
        {
            var exQuote = erpNodeDBContext.Estimates.Find(est.Id);

            if (exQuote != null)
            {
                exQuote.Reference = est.Reference;
                exQuote.Memo = est.Memo;

                exQuote.ProjectId = est.ProjectId;
                exQuote.TrnDate = est.TrnDate;
                exQuote.PaymentTermId = est.PaymentTermId;
                exQuote.ShippingTermId = est.ShippingTermId;

                exQuote.TaxCodeId = est.TaxCodeId;
                exQuote.TaxCode = organization.TaxCodes.Find(est.TaxCodeId);

                exQuote.UpdateName();
                exQuote.Calculate();


                erpNodeDBContext.SaveChanges();
                return est;
            }

            return null;
        }

        public void Delete(Guid id)
        {
            var PurchasesQuote = erpNodeDBContext.Estimates.Find(id);

            foreach (var item in PurchasesQuote.Items.ToList())
            {
                PurchasesQuote.Items.Remove(item);
            }
            organization.SaveChanges();
            erpNodeDBContext.Estimates.Remove(PurchasesQuote);
            organization.SaveChanges();
        }


        public Quote Copy(Quote originalPurchaseQuote, DateTime trDate)
        {
            var clonePurchaseQuote = this.erpNodeDBContext.PurchaseEstimates
                    .AsNoTracking()
                    .Include(p => p.Items)
                    .FirstOrDefault(x => x.Id == originalPurchaseQuote.Id);

            clonePurchaseQuote.Id = Guid.NewGuid();
            clonePurchaseQuote.TrnDate = trDate;
            clonePurchaseQuote.Reference = "Clone-" + clonePurchaseQuote.Reference;
            clonePurchaseQuote.No = organization.PurchaseEstimates.NextNumber;
            clonePurchaseQuote.Status = QuoteStatus.Quote;
            clonePurchaseQuote.Items.ToList().ForEach(ci => ci.Id = Guid.NewGuid());


            this.erpNodeDBContext.Estimates.Add(clonePurchaseQuote);
            this.erpNodeDBContext.SaveChanges();

            return clonePurchaseQuote;
        }

        internal void UpdateDoumentsName()
        {
            erpNodeDBContext.Estimates.ToList().ForEach(c =>
            {
                Console.Write("#");
                c.UpdateName();
            });
            erpNodeDBContext.SaveChanges();
        }
    }
}