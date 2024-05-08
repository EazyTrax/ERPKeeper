
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;

namespace ERPKeeperCore.Enterprise.DAL.Taxes
{
    public class TaxPeriods : ERPNodeDalRepository
    {
        public TaxPeriods(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<TaxPeriod> ListAll()
        {
            return erpNodeDBContext.TaxPeriods.ToList();
        }

        public TaxPeriod? Find(Guid Id) => erpNodeDBContext.TaxPeriods.Find(Id);

        public void Refresh()
        {
            var taxPeriods = erpNodeDBContext.TaxPeriods.ToList();

            foreach (var taxPeriod in taxPeriods)
            {
                taxPeriod.UpdateBalance();
                erpNodeDBContext.SaveChanges();
            }
        }

        public int Count()
        {
            return erpNodeDBContext.TaxPeriods.Count();
        }

    }
}