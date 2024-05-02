using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{

    [Route("/{CompanyId}/{area}/Sales/{TransactionId:Guid}/{action=index}")]
    public class SaleController : _Profiles_Customers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Items()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }



        public IActionResult Documents()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }
    }
}
