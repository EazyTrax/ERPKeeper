
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class FiscalYearAccountBalances : ERPNodeDalRepository
    {
        public FiscalYearAccountBalances(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<FiscalYearAccountBalance> GetAll()
        {
            return erpNodeDBContext.FiscalYearAccountBalances.ToList();
        }



        public FiscalYearAccountBalance? Find(Guid Id) => erpNodeDBContext.FiscalYearAccountBalances.Find(Id);


    }
}