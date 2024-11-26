
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
using ERPKeeperCore.Enterprise.Models.Profiles;

namespace ERPKeeperCore.Enterprise.DAL.Suppliers
{
    public class Suppliers : ERPNodeDalRepository
    {
        public Suppliers(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Supplier> GetAll()
        {
            return erpNodeDBContext.Suppliers.ToList();
        }


        public int Count() => erpNodeDBContext.Suppliers.Count();
        public Supplier? Find(Guid Id) => erpNodeDBContext.Suppliers.Find(Id);

        public void UpdatePurchasesBalance()
        {

            erpNodeDBContext.Suppliers.ToList()
                    .ForEach(x =>
                    {
                        x.UpdateBalance();
                    });
            erpNodeDBContext.SaveChanges();
        }

        public void Add(Profile profile)
        {
            var supplier = new Supplier
            {
                Id = profile.Id,
                Profile = profile,
                Code = profile.ShotName,
                Status = Models.ProfileStatus.Active,
            };

            erpNodeDBContext.Suppliers.Add(supplier);
            erpNodeDBContext.SaveChanges();
        }

        public void UpdatePurchasesAndPurchaseQuoteCount()
        {

            var suppliers = GetAll();
            var purchases = erpNodeDBContext.Purchases
                .Where(p => suppliers.Select(s => s.Id).Contains(p.SupplierId))
                .ToList();

            // Group purchases by SupplierId for easier aggregation
            var groupedPurchases = purchases.GroupBy(p => p.SupplierId).ToDictionary(g => g.Key);

            // Iterate over each supplier and calculate aggregates from in-memory data
            foreach (var supplier in suppliers)
            {
                if (groupedPurchases.TryGetValue(supplier.Id, out var supplierPurchases))
                {
                    supplier.TotalPurchases = supplierPurchases.Sum(p => p.Total);
                    supplier.TotalPurchasesCount = supplierPurchases.Count();

                    supplier.TotalBalance = supplierPurchases
                        .Where(p => p.Status == PurchaseStatus.Open)
                        .Sum(p => p.Total);

                    supplier.TotalBalanceCount = supplierPurchases
                        .Count(p => p.Status == PurchaseStatus.Open);
                }
                else
                {
                    supplier.TotalPurchases = 0;
                    supplier.TotalPurchasesCount = 0;
                    supplier.TotalBalance = 0;
                    supplier.TotalBalanceCount = 0;
                }
            }

            // Save all changes once
            erpNodeDBContext.SaveChanges();


        }
    }
}