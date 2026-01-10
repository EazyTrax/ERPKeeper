using ERPKeeperCore.Web.Areas.Financial.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Financials.Controllers
{

    [Route("/Financial/RetentionTypes/{Id:Guid}/{action=index}")]
    public class RetentionTypeController : Financial_BaseController
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.RetentionTypes.Find(Id);

            if (transcation == null)
                return NotFound();


            return View(transcation);
        }




        public IActionResult Export()
        {
            var transcation = Organization.RetentionTypes.Find(Id);
            return View(transcation);
        }
    }
}