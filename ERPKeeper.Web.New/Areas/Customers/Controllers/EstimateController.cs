using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/{area}/Estimates/{EstimateId:Guid}/{action=index}")]
    public class EstimateController : Base_CustomersController
    {

        public Guid EstimateId => Guid.Parse(RouteData.Values["EstimateId"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.SaleEstimates.Find(EstimateId);
            return View(transcation);
        }


        public IActionResult Items()
        {
            var transcation = Organization.SaleEstimates.Find(EstimateId);
            return View(transcation);
        }
        public IActionResult Documents()
        {
            var transcation = Organization.SaleEstimates.Find(EstimateId);
            return View(transcation);
        }

    }
}
