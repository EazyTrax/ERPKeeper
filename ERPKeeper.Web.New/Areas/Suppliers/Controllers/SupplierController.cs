﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/Suppliers/Suppliers/{supplierUid:Guid}/{action=Index}")]
    public class SupplierController : _Suppliers_Base_Controller
    {
        public IActionResult Index(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }

        public IActionResult Purchases(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }

        public IActionResult PurchaseQuotes(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }
        public IActionResult Payments(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }
        public IActionResult Items(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }

        public IActionResult EstimateItems(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }



    }
}
