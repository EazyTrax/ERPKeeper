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

    [Route("/{CompanyId}/Financial/LendReturns/{Id:Guid}/{action=index}")]
    public class LendReturnController : Financial_BaseController
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.LendReturns.Find(Id);

            if (transcation == null)
                return NotFound();


            return View(transcation);
        }

        public IActionResult UnPost()
        {
            var model = Organization.LendReturns.Find(Id);
            Organization.LendReturns.UnPost(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Export()
        {
            var transcation = Organization.Lends.Find(Id);
            return View(transcation);
        }
    }
}