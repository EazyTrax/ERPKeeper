﻿
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Investors;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Investors : ERPNodeDalRepository
    {
        public Investors(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Investor> GetAll()
        {
            return erpNodeDBContext.Investors.ToList();
        }

        public int Count() => erpNodeDBContext.Investors.Count();

        public Investor? Find(Guid Id) => erpNodeDBContext.Investors.Find(Id);


    }
}