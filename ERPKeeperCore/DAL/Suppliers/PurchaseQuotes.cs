

using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;

namespace ERPKeeperCore.Enterprise.DAL.Suppliers
{
    public class PurchaseQuotes : ERPNodeDalRepository
    {
        public PurchaseQuotes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<PurchaseQuote> GetAll()
        {
            return erpNodeDBContext.PurchaseQuotes.ToList();
        }



        public PurchaseQuote? Find(Guid Id) => erpNodeDBContext.PurchaseQuotes.Find(Id);

        public void New(PurchaseQuote model)
        {
            var currentYear = model.Date.Year;
            var currentMonth = model.Date.Month;

            var maxNo = erpNodeDBContext.PurchaseQuotes
                .Where(a => a.Date.Year == currentYear && a.Date.Month == currentMonth)
                .Select(a => (int?)a.No)
                .Max() ?? 0;

            model.No = maxNo + 1;
            model.UpdateBalance();
            model.UpdateName();

            erpNodeDBContext.PurchaseQuotes.Add(model);
            erpNodeDBContext.SaveChanges();
        }

        public PurchaseQuote Create(PurchaseQuote model, Guid? SupplierId = null)
        {
            if (SupplierId != null)
                model.SupplierId = (Guid)SupplierId;

            var maxNo = erpNodeDBContext.PurchaseQuotes
                .Select(a => (int?)a.No)
                .Max() ?? 0;

            model.Date = DateTime.Today;
            model.Status = PurchaseQuoteStatus.Open;
            model.No = maxNo + 1;
            model.UpdateBalance();
            model.UpdateName();

            erpNodeDBContext.PurchaseQuotes.Add(model);
            erpNodeDBContext.SaveChanges();

            return model;
        }

        public void Delete(PurchaseQuote transcation)
        {

            transcation.Items.Clear();
            erpNodeDBContext.PurchaseQuotes.Remove(transcation);
            erpNodeDBContext.SaveChanges();

        }

        public void Remove(List<PurchaseQuote> expiredSPurchaseQuotes)
        {
            erpNodeDBContext.PurchaseQuotes.RemoveRange(expiredSPurchaseQuotes);
            erpNodeDBContext.SaveChanges();
        }

        public void RemoveExpired(int days)
        {
            var expiredQuotes = erpNodeDBContext.PurchaseQuotes
                .ToList()
                        .Where(q => q.AgeInDays > days)
                        .ToList();

            expiredQuotes.ForEach(s =>
            {
                s.Items.Clear();
                erpNodeDBContext.PurchaseQuotes.Remove(s);
            });

            erpNodeDBContext.PurchaseQuotes.RemoveRange(expiredQuotes);

        }
    }
}