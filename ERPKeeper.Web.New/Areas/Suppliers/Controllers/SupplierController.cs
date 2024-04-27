using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/{area}/Suppliers/{supplierUid:Guid}/{action=Index}")]
    public class SupplierController : Base_SuppliersController
    {
        public IActionResult Index(Guid supplierUid)
        {
            var supplier = EnterpriseRepo.Suppliers.Find(supplierUid);
            return View(supplier);
        }
        public IActionResult Estimates(Guid supplierUid)
        {
            var supplier = EnterpriseRepo.Suppliers.Find(supplierUid);
            return View(supplier);
        }
        public IActionResult Purchases(Guid supplierUid)
        {
            var supplier = EnterpriseRepo.Suppliers.Find(supplierUid);
            return View(supplier);
        }

        public IActionResult Items(Guid supplierUid)
        {
            var supplier = EnterpriseRepo.Suppliers.Find(supplierUid);
            return View(supplier);
        }


      
    }
}
