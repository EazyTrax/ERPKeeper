using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.ChartOfAccount;
using ERPKeeper.Node.Models.Estimations;
using ERPKeeper.Node.Models.Estimations.Enums;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ERPKeeper.Node.DAL.Quotes
{
    public class Quotes : ERPNodeDalRepository
    {
        public Quotes(Organization organization) : base(organization)
        {

        }

        public List<Quote> GetAll()
        {
            return erpNodeDBContext.Quotes.ToList();
        }

        public IQueryable<Quote> Query() => erpNodeDBContext.Quotes;
        public Quote Find(Guid quoteId) => erpNodeDBContext.Quotes.Find(quoteId);
        public void Remove(Quote entity) => erpNodeDBContext.Quotes.Remove(entity);
        public int NextNumber => (erpNodeDBContext.Quotes.Max(e => (int?)e.No) ?? 0) + 1;
        public List<Quote> ListByStatus(QuoteStatus? status)
        {
            if (status == null)
              return   erpNodeDBContext.Quotes.ToList();
            else
                return erpNodeDBContext.Quotes.Where(e => e.Status == status).ToList();
        }

        public Quote Create(Guid profileGuid, DateTime createDate)
        {
            var newPurchaseQuote = new Quote()
            {
                TransactionType = ERPObjectType.PurchaseQuote,
                Uid = Guid.NewGuid(),
                No = this.NextNumber,
                ProfileGuid = profileGuid,
                TrnDate = createDate,
                TaxCode = organization.TaxCodes.GetDefaultInput,
                SelfProfileGuid = organization.SelfProfile.Uid,
                ExpiredInDayCount = 90,
            };

            erpNodeDBContext.Quotes.Add(newPurchaseQuote);
            erpNodeDBContext.SaveChanges();

            return newPurchaseQuote;
        }

        public void ReOrder()
        {
            var transfers = erpNodeDBContext.Quotes
              .OrderBy(t => t.TrnDate)
              .ThenBy(t => t.No)
              .ToList();

            int i = 0;
            transfers.ForEach(t => t.No = i++);

            erpNodeDBContext.SaveChanges();
        }

        public Quote SaveChanges(Quote est)
        {
            var exQuote = erpNodeDBContext.Quotes.Find(est.Uid);

            if (exQuote != null)
            {
                exQuote.Reference = est.Reference;
                exQuote.Memo = est.Memo;

                exQuote.ProjectGuid = est.ProjectGuid;
                exQuote.TrnDate = est.TrnDate;
                exQuote.PaymentTermGuid = est.PaymentTermGuid;
                exQuote.ShippingTermGuid = est.ShippingTermGuid;

                exQuote.TaxCodeGuid = est.TaxCodeGuid;
                exQuote.TaxCode = organization.TaxCodes.Find(est.TaxCodeGuid);

                exQuote.UpdateName();
                exQuote.Calculate();


                erpNodeDBContext.SaveChanges();
                return est;
            }

            return null;
        }

        public void Delete(Guid id)
        {
            var PurchasesQuote = erpNodeDBContext.Quotes.Find(id);

            foreach (var item in PurchasesQuote.Items.ToList())
            {
                PurchasesQuote.Items.Remove(item);
            }
            organization.SaveChanges();
            erpNodeDBContext.Quotes.Remove(PurchasesQuote);
            organization.SaveChanges();
        }


        public Quote Copy(Quote originalPurchaseQuote, DateTime trDate)
        {
            var clonePurchaseQuote = this.erpNodeDBContext.Quotes
                    .AsNoTracking()
                    .Include(p => p.Items)
                    .FirstOrDefault(x => x.Uid == originalPurchaseQuote.Uid);

            clonePurchaseQuote.Uid = Guid.NewGuid();
            clonePurchaseQuote.TrnDate = trDate;
            clonePurchaseQuote.Reference = "Clone-" + clonePurchaseQuote.Reference;
            clonePurchaseQuote.No = organization.PurchaseQuotes.NextNumber;
            clonePurchaseQuote.Status = QuoteStatus.Quote;
            clonePurchaseQuote.Items.ToList().ForEach(ci => ci.Uid = Guid.NewGuid());


            this.erpNodeDBContext.Quotes.Add(clonePurchaseQuote);
            this.erpNodeDBContext.SaveChanges();

            return clonePurchaseQuote;
        }

        internal void UpdateDoumentsName()
        {
            erpNodeDBContext.Quotes.ToList().ForEach(c =>
            {
                Console.Write("#");
                c.UpdateName();
            });
            erpNodeDBContext.SaveChanges();
        }
    }
}