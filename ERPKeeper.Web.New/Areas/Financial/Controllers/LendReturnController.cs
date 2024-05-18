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
            var transcation = OrganizationCore.LendReturns.Find(Id);

            if (transcation == null)
                return NotFound();


            return View(transcation);
        }

        public IActionResult UnPost()
        {
            var model = OrganizationCore.LendReturns.Find(Id);
            OrganizationCore.LendReturns.UnPost(model);
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Export()
        {
            var transcation = OrganizationCore.Lends.Find(Id);
            return View(transcation);
        }
    }
}