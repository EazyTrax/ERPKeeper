

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

            erpNodeDBContext.PurchaseQuotes.Add(model);
            erpNodeDBContext.SaveChanges();
        }
    }
}