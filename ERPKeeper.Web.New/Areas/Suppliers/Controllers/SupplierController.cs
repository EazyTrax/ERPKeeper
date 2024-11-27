using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;
using ERPKeeperCore.Enterprise.Models.Suppliers;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/Suppliers/Suppliers/{supplierId:Guid}/{action=Index}")]
    public class SupplierController : _Suppliers_Base_Controller
    {
        public IActionResult Index(Guid supplierId)
        {
            var supplier = Organization.Suppliers.Find(supplierId);
            return View(supplier);
        }

        public IActionResult Purchases(Guid supplierId)
        {
            var supplier = Organization.Suppliers.Find(supplierId);
            return View(supplier);
        }

        public IActionResult PurchaseQuotes(Guid supplierId)
        {
            var supplier = Organization.Suppliers.Find(supplierId);
            return View(supplier);
        }
        public IActionResult Payments(Guid supplierId)
        {
            var supplier = Organization.Suppliers.Find(supplierId);
            return View(supplier);
        }
        public IActionResult Items(Guid supplierId)
        {
            var supplier = Organization.Suppliers.Find(supplierId);
            return View(supplier);
        }

        public IActionResult EstimateItems(Guid supplierId)
        {
            var supplier = Organization.Suppliers.Find(supplierId);
            return View(supplier);
        }
        public IActionResult UpdateItems(Guid supplierId)
        {
            // Retrieve Supplier once
            var supplier = Organization.Suppliers.Find(supplierId);
            if (supplier == null)
                return NotFound();

            // Fetch SupplierItems in bulk to avoid querying in each loop
            var supplierItemsDict = Organization.ErpCOREDBContext.SupplierItems
                .Where(ct => ct.SupplierId == supplierId)
                .ToDictionary(ct => ct.ItemId, ct => ct);

            // Calculate and update AmountPurchase from PurchaseItems
            var purchaseItems = Organization.ErpCOREDBContext.PurchaseItems
                .Where(si => si.Purchase.SupplierId == supplierId)
                .GroupBy(si => si.ItemId)
                .Select(si => new { ItemId = si.Key, Amount = si.Sum(x => x.Quantity) })
                .ToList();

            foreach (var item in purchaseItems)
            {
                if (!supplierItemsDict.TryGetValue(item.ItemId, out var supplierItem))
                {
                    // Create and add new SupplierItem if it doesn't exist
                    supplierItem = new Enterprise.Models.Suppliers.SupplierItem(item.ItemId, supplierId);
                    supplier.SupplierItems.Add(supplierItem);
                    supplierItemsDict[item.ItemId] = supplierItem;
                }
                supplierItem.AmountPurchase = item.Amount;
            }

            // Calculate and update AmountPurchaseQuote from PurchaseQuoteItems
            var quoteItems = Organization.ErpCOREDBContext.PurchaseQuoteItems
                .Where(si => si.SupplierQuote.SupplierId == supplierId)
                .GroupBy(si => si.ItemId)
                .Select(si => new { ItemId = si.Key, Amount = si.Sum(x => x.Quantity) })
                .ToList();

            foreach (var item in quoteItems)
            {
                if (!supplierItemsDict.TryGetValue(item.ItemId, out var supplierItem))
                {
                    // Create and add new SupplierItem if it doesn't exist
                    supplierItem = new Enterprise.Models.Suppliers.SupplierItem(item.ItemId, supplierId);
                    supplier.SupplierItems.Add(supplierItem);
                    supplierItemsDict[item.ItemId] = supplierItem;
                }
                supplierItem.AmountPurchaseQuote = item.Amount;
            }

            // Save all changes in one go
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult Update(Guid supplierId, Supplier model)
        {
            var supplier = Organization.Suppliers.Find(supplierId);

            supplier.DefaultExpenseAccountId = model.DefaultExpenseAccountId;
            supplier.DefaultTaxCodeUid = model.DefaultTaxCodeUid;
            supplier.DefaultProductItemId = model.DefaultProductItemId;


            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
