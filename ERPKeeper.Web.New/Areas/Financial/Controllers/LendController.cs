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

    [Route("/{CompanyId}/Financial/Lends/{Id:Guid}/{action=index}")]
    public class LendController : Financial_BaseController
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = OrganizationCore.Lends.Find(Id);
            return View(transcation);
        }

        public IActionResult UnPost()
        {
            var model = OrganizationCore.Lends.Find(Id);
            OrganizationCore.Lends.UnPost(model);
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Items()
        {
            var transcation = OrganizationCore.Lends.Find(Id);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = OrganizationCore.Lends.Find(Id);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = OrganizationCore.Lends.Find(Id);
            return View(transcation);
        }

        public IActionResult Documents()
        {
            var transcation = OrganizationCore.Lends.Find(Id);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = OrganizationCore.Lends.Find(Id);
            return View(transcation);
        }
    }
}