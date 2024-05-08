using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{

    [Route("/{CompanyId}/Profiles/Customers/ReceivePayments/{TransactionId:Guid}/{action=index}")]
    public class ReceivePaymentController : _Profiles_Customers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var receivePayment = OrganizationCore.ErpCOREDBContext.ReceivePayments.Find(TransactionId);
            return View(receivePayment);
        }
    }
}
