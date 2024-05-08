using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Suppliers.Controllers
{

    [Route("/{CompanyId}/Profiles/Suppliers/SupplierPayments/{TransactionId:Guid}/{action=index}")]
    public class SupplierPaymentController : _Profiles_Suppliers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var SupplierPayment = OrganizationCore.ErpCOREDBContext.SupplierPayments.Find(TransactionId);
            return View(SupplierPayment);
        }
    }
}
