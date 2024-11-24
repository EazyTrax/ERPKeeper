using ERPKeeperCore.Web.Areas.Customers.Controllers;
using ERPKeeperCore.Web.Areas.Investors.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Projects.Controllers
{

    public class ProjectsController : _Projects_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Route("/{CompanyId}/Projects/Refresh")]
        public IActionResult Refresh()
        {
            Organization.Projects.UpdateBalance();

            return Redirect($"/{CompanyId}/Projects");
        }

    }
}
