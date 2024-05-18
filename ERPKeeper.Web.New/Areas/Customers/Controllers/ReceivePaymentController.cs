using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    [Route("/{CompanyId}/Customers/ReceivePayments/{TransactionId:Guid}/{action=index}")]
    public class ReceivePaymentController : _Customers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var receivePayment = OrganizationCore.ErpCOREDBContext.ReceivePayments.Find(TransactionId);
            return View(receivePayment);
        }

        public IActionResult Export()
        {
            var receivePayment = OrganizationCore.ErpCOREDBContext.ReceivePayments.Find(TransactionId);
            return View(receivePayment);
        }

        public IActionResult Update()
        {
            var receivePayment = OrganizationCore.ErpCOREDBContext.ReceivePayments.Find(TransactionId);

            receivePayment.UpdateBalance();
            OrganizationCore.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
