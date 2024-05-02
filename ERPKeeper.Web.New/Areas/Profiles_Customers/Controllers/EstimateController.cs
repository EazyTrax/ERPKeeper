using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{
    [Route("/{CompanyId}/{area}/Estimates/{EstimateId:Guid}/{action=index}")]
    public class EstimateController : _Profiles_Customers_Base_Controller
    {

        public Guid EstimateId => Guid.Parse(RouteData.Values["EstimateId"].ToString());

        public IActionResult Index()
        {
            var transcation = EnterpriseRepo.Sales.Find(EstimateId);
            return View(transcation);
        }


        public IActionResult Items()
        {
            var transcation = EnterpriseRepo.Sales.Find(EstimateId);
            return View(transcation);
        }
        public IActionResult Documents()
        {
            var transcation = EnterpriseRepo.Sales.Find(EstimateId);
            return View(transcation);
        }

    }
}
