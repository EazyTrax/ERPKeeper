﻿
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

namespace ERPKeeperCore.Enterprise.DAL.Customers
{
    public class Customers : ERPNodeDalRepository
    {
        public Customers(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Customer> GetAll()
        {
            return erpNodeDBContext.Customers.ToList();
        }

        public int Count() => erpNodeDBContext.Customers.Count();


        public Customer? Find(Guid Id) => erpNodeDBContext.Customers.Find(Id);

        public void UpdateSalesBalance()
        {

                    erpNodeDBContext.Customers.ToList()
                            .ForEach(x =>
                            {
                                x.UpdateBalance();
                            });


            erpNodeDBContext.SaveChanges();
        }
    }
}