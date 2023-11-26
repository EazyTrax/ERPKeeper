using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/{area}/Suppliers/{supplierUid:Guid}/{action=Index}")]
    public class SupplierController : Base_SuppliersController
    {
        public IActionResult Index(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }
        public IActionResult Estimates(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }
        public IActionResult Purchases(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }

        public IActionResult Items(Guid supplierUid)
        {
            var supplier = Organization.Suppliers.Find(supplierUid);
            return View(supplier);
        }


      
    }
}
