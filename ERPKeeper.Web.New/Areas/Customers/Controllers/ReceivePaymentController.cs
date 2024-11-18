using ERPKeeperCore.Enterprise.Models.Customers;
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

            var receivePayment = Organization.ErpCOREDBContext.ReceivePayments.Find(TransactionId);
            return View(receivePayment);
        }

        public IActionResult Export()
        {
            var receivePayment = Organization.ErpCOREDBContext.ReceivePayments.Find(TransactionId);
            return View(receivePayment);
        }

        public IActionResult Update(ReceivePayment model)
        {
            var receivePayment = Organization.ErpCOREDBContext.ReceivePayments.Find(TransactionId);

            receivePayment.Date = model.Date;
            receivePayment.Reference = model.Reference;

            receivePayment.RetentionTypeId = model.RetentionTypeId;
            receivePayment.Receivable_Asset_AccountId = model.Receivable_Asset_AccountId;

            receivePayment.AmountBankFee = model.AmountBankFee;
            receivePayment.AmountDiscount = model.AmountDiscount;
            receivePayment.Memo = model.Memo;


            receivePayment.UpdateBalance();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
