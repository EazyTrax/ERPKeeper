
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
using ERPKeeperCore.Enterprise.Models.Profiles;
using ERPKeeperCore.Enterprise.Models;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using Azure.Core;

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

        public void Add(Profile profile)
        {
            var customer = new Customer
            {
                Id = profile.Id,
                Profile = profile,
                Code = profile.ShotName,
                Status = ProfileStatus.Active,
            };

            erpNodeDBContext.Customers.Add(customer);
            erpNodeDBContext.SaveChanges();
        }

        public void UpdateSalesAndSaleQuoteCount()
        {


            var customers = GetAll();
            var Sales = erpNodeDBContext.Sales
                .Where(p => customers.Select(s => s.Id).Contains(p.CustomerId))
                .ToList();

            // Group Sales by CustomerId for easier aggregation
            var groupedSales = Sales.GroupBy(p => p.CustomerId).ToDictionary(g => g.Key);

            // Iterate over each supplier and calculate aggregates from in-memory data
            foreach (var supplier in customers)
            {
                if (groupedSales.TryGetValue(supplier.Id, out var supplierSales))
                {
                    supplier.TotalSales = supplierSales.Sum(p => p.Total);
                    supplier.TotalSalesCount = supplierSales.Count();

                    supplier.TotalBalance = supplierSales
                        .Where(p => p.Status == SaleStatus.Open)
                        .Sum(p => p.Total);

                    supplier.TotalBalanceCount = supplierSales
                        .Count(p => p.Status == SaleStatus.Open);
                }
                else
                {
                    supplier.TotalSales = 0;
                    supplier.TotalSalesCount = 0;
                    supplier.TotalBalance = 0;
                    supplier.TotalBalanceCount = 0;
                }
            }

            // Save all changes once
            erpNodeDBContext.SaveChanges();



        }

        public void RemoveNoSaleOrQuote()
        {
            var noSalesCustomer = erpNodeDBContext.Customers
                  .Where(p => p.Sales.Count == 0 && p.SaleQuotes.Count == 0)
                  .ToList();

            erpNodeDBContext.Customers.RemoveRange(noSalesCustomer);
            erpNodeDBContext.SaveChanges();
        }

        public bool Remove(Customer customer)
        {
            if (customer.Sales.Count > 0)
                return false;

            customer.SaleQuotes.Clear();
            customer.CustomerItems.Clear();
            erpNodeDBContext.SaveChanges();

            erpNodeDBContext.Customers.Remove(customer);
            erpNodeDBContext.SaveChanges();

            return true;
        }
    }
}