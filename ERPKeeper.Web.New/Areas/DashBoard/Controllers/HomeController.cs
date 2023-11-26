using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseNodeController
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/{CompanyId}/{action}")]
        public IActionResult Migrate()
        {
            var Organization = new Node.DAL.Organization(CompanyId, true);
            var profiles = Organization.ErpNodeDBContext.Profiles;
            return Content($"{profiles.Count()}");
        }

    }
}
