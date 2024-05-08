
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
    public class TaxCodes : ERPNodeDalRepository
    {
        public TaxCodes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<TaxCode> ListAll()
        {
            return erpNodeDBContext.TaxCodes.ToList();
        }



        public TaxCode? Find(Guid Id) => erpNodeDBContext.TaxCodes.Find(Id);

        public int Count()
        {
            return erpNodeDBContext.TaxCodes.Count();
        }
    }
}