using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{
    public class PurchaseQuotesController : _Suppliers_Base_Controller
    {
        [Route("/Suppliers/{controller}")]
        [Route("/Suppliers/{controller}/{status}")]
        public IActionResult Index(PurchaseQuoteStatus status = PurchaseQuoteStatus.Open)
        {
          
            return View("Index", status);
        }
    }
}
