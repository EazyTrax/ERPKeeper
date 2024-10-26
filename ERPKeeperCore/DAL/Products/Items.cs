
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ERPKeeperCore.Enterprise.Models.Customers;

namespace ERPKeeperCore.Enterprise.DAL.Products
{
    public class Items : ERPNodeDalRepository
    {
        public Items(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Item> GetAll()
        {
            return erpNodeDBContext.Items.ToList();
        }

        public int Count()
        {
            return erpNodeDBContext.Items.Count();
        }

        public Item? Find(Guid Id) => erpNodeDBContext.Items.Find(Id);

        public object Copy(Item item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }







        public void UpdateSupplierItems()
        {
            var saleItems = erpNodeDBContext.PurchaseItems
                .ToList()
                 .GroupBy(si => new { ItemId = si.ItemId, SupplierId = si.Purchase.SupplierId })
                 .Select(si => new
                 {
                     ItemId = si.Key.ItemId,
                     SupplierId = si.Key.SupplierId,
                     Amount = si.Sum(x => x.Quantity),
                 }).ToList();

            saleItems.ForEach(si =>
            {

                var supplierItem = erpNodeDBContext.SupplierItems
                .Where(ct => ct.SupplierId == si.SupplierId && ct.ItemId == si.ItemId)
                .FirstOrDefault();

                if (supplierItem == null)
                {
                    supplierItem = new Enterprise.Models.Suppliers.SupplierItem(si.ItemId, si.SupplierId);
                    erpNodeDBContext.SupplierItems.Add(supplierItem);
                }

                supplierItem.AmountPurchase = si.Amount;
            });

            erpNodeDBContext.SaveChanges();


            var quoteItems = erpNodeDBContext.PurchaseQuoteItems
                 .GroupBy(si => new { ItemId = si.ItemId, SupplierId = si.SupplierQuote.SupplierId })
                 .Select(si => new
                 {
                     ItemId = si.Key.ItemId,
                     SupplierId = si.Key.SupplierId,
                     Amount = si.Sum(x => x.Quantity),
                 }).ToList();

            quoteItems.ForEach(si =>
            {
                var supplierItem = erpNodeDBContext.SupplierItems
                .Where(ct => ct.SupplierId == si.SupplierId && ct.ItemId == si.ItemId)
                .FirstOrDefault();

                if (supplierItem == null)
                {
                    supplierItem = new Enterprise.Models.Suppliers.SupplierItem(si.ItemId, si.SupplierId);
                    erpNodeDBContext.SupplierItems.Add(supplierItem);
                }

                supplierItem.AmountPurchaseQuote = si.Amount;
            });

            erpNodeDBContext.SaveChanges();
        }




        public void UpdateCustomerItems()
        {
            var saleItems = erpNodeDBContext.SaleItems
                 .GroupBy(si => new { ItemId = si.ItemId, CustomerId = si.Sale.CustomerId })
                 .Select(si => new
                 {
                     ItemId = si.Key.ItemId,
                     CustomerId = si.Key.CustomerId,
                     Amount = si.Sum(x => x.Quantity),
                 }).ToList();

            saleItems.ForEach(si =>
            {
                var customerItem = erpNodeDBContext.CustomerItems
                .Where(ct => ct.CustomerId == si.CustomerId && ct.ItemId == si.ItemId)
                .FirstOrDefault();

                if (customerItem == null)
                {
                    customerItem = new Enterprise.Models.Customers.CustomerItem(si.ItemId, si.CustomerId);
                    erpNodeDBContext.CustomerItems.Add(customerItem);
                }

                customerItem.AmountSale = si.Amount;
            });

            erpNodeDBContext.SaveChanges();


            var quoteItems = erpNodeDBContext.SaleQuoteItems
                 .GroupBy(si => new { ItemId = si.ItemId, CustomerId = si.Quote.CustomerId })
                 .Select(si => new
                 {
                     ItemId = si.Key.ItemId,
                     CustomerId = si.Key.CustomerId,
                     Amount = si.Sum(x => x.Quantity),
                 }).ToList();

            quoteItems.ForEach(si =>
            {
                var customerItem = erpNodeDBContext.CustomerItems
                .Where(ct => ct.CustomerId == si.CustomerId && ct.ItemId == si.ItemId)
                .FirstOrDefault();

                if (customerItem == null)
                {
                    customerItem = new Enterprise.Models.Customers.CustomerItem(si.ItemId, si.CustomerId);
                    erpNodeDBContext.CustomerItems.Add(customerItem);
                }

                customerItem.AmountSaleQuote = si.Amount;
            });

            erpNodeDBContext.SaveChanges();
        }

        public void RemoveGroupItems()
        {
            var groupItems = erpNodeDBContext.Items.Where(i => i.ItemType == ItemTypes.Group).ToList();
            erpNodeDBContext.Items.RemoveRange(groupItems);
            erpNodeDBContext.SaveChanges();
        }

        public void Update_Sales_Amount()
        {
            // Group SaleItems and calculate total Amount for each ItemId
            var saleItemsAmount = erpNodeDBContext.SaleItems.ToList()
                .GroupBy(si => si.ItemId)
                .Select(g => new { Id = g.Key, Amount = g.Sum(s => s.Quantity), Value = g.Sum(s=>s.LineTotal) })
                .ToList();

            // Fetch all items that need updating in a single query
            var itemsToUpdate = erpNodeDBContext.Items
                .Where(i => saleItemsAmount.Select(si => si.Id).Contains(i.Id))
                .ToList();

            // Update the AmountSold for each item in-memory
            foreach (var saleItemAmount in saleItemsAmount)
            {
                var item = itemsToUpdate.FirstOrDefault(i => i.Id == saleItemAmount.Id);
                if (item != null)
                {
                    item.SoldAmount = saleItemAmount.Amount;
                    item.SoldValue = saleItemAmount.Value;
                    item.AmountOnHand = item.PurchaseAmount - item.SoldAmount;
                }
            }

            // Save all changes in one go
            erpNodeDBContext.SaveChanges();
        }

        public void Update_Purchases_Amount()
        {
            // Group SaleItems and calculate total Amount for each ItemId
            var saleItemsAmount = erpNodeDBContext.PurchaseItems.ToList()
                .GroupBy(si => si.ItemId)
                .Select(g => new { Id = g.Key, Amount = g.Sum(s => s.Quantity), Value = g.Sum(s => s.LineTotal) })
                .ToList();

            // Fetch all items that need updating in a single query
            var itemsToUpdate = erpNodeDBContext.Items
                .Where(i => saleItemsAmount.Select(si => si.Id).Contains(i.Id))
                .ToList();

            // Update the AmountSold for each item in-memory
            foreach (var saleItemAmount in saleItemsAmount)
            {
                var item = itemsToUpdate.FirstOrDefault(i => i.Id == saleItemAmount.Id);
                if (item != null)
                {
                    item.PurchaseAmount = saleItemAmount.Amount;
                    item.PurchaseValue = saleItemAmount.Value;
                    item.AmountOnHand = item.PurchaseAmount - item.SoldAmount;
                }
            }

            // Save all changes in one go
            erpNodeDBContext.SaveChanges();
        }



    }
}