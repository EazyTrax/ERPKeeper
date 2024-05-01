using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Dashboard.Controllers
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
            var Organization = new Enterprise.EnterpriseRepo(CompanyId, true);
            var profiles = Organization.ErpCOREDBContext.Profiles;
            return Content($"{profiles.Count()}");
        }

    }
}
