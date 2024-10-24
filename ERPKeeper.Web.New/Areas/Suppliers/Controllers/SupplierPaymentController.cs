using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/Suppliers/SupplierPayments/{TransactionId:Guid}/{action=index}")]
    public class SupplierPaymentController : _Suppliers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var SupplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(TransactionId);
            return View(SupplierPayment);
        }
    }
}
