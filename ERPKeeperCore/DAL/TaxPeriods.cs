﻿
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

namespace ERPKeeperCore.Enterprise.DAL
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


    }
}