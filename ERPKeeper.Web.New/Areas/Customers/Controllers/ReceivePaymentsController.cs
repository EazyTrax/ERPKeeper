using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    public class ReceivePaymentsController : _Customers_Base_Controller
    {
        public IActionResult Index()
        {
            Organization.ReceivePayments.CreateTransactions();
            return View();
        }
        public IActionResult Refresh()
        {
            Organization.ReceivePayments.UpdateRetentionGroups();
  

            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
