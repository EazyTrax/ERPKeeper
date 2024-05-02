using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{
    [Route("/{CompanyId}/Profiles/Customers/Quotes/{QuoteId:Guid}/{action=index}")]
    public class QuoteController : _Profiles_Customers_Base_Controller
    {

        public Guid QuoteId => Guid.Parse(RouteData.Values["QuoteId"].ToString());

        public IActionResult Index()
        {
            var transcation = EnterpriseRepo.Sales.Find(QuoteId);
            return View(transcation);
        }


        public IActionResult Items()
        {
            var transcation = EnterpriseRepo.Sales.Find(QuoteId);
            return View(transcation);
        }
        public IActionResult Documents()
        {
            var transcation = EnterpriseRepo.Sales.Find(QuoteId);
            return View(transcation);
        }

    }
}
