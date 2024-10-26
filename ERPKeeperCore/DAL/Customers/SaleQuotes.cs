

using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Enterprise.DAL.Customers
{
    public class SaleQuotes : ERPNodeDalRepository
    {
        public SaleQuotes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<SaleQuote> GetAll()
        {
            return erpNodeDBContext.SaleQuotes.ToList();
        }

        public SaleQuote? Find(Guid Id) => erpNodeDBContext.SaleQuotes.Find(Id);

        public SaleQuote CreateDraft(SaleQuote model, Guid? customerId = null)
        {
            if (customerId != null)
                model.CustomerId = (Guid)customerId;

            var maxNo = erpNodeDBContext.SaleQuotes
                .Select(a => (int?)a.No)
                .Max() ?? 0;

            model.Date = DateTime.Now;
            model.Status = SaleQuoteStatus.Draft;
            model.No = maxNo + 1;
            model.UpdateBalance();
            model.UpdateName();

            erpNodeDBContext.SaleQuotes.Add(model);
            erpNodeDBContext.SaveChanges();

            return model;
        }

    }
}