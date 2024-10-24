using ERPKeeperCore.Web.Areas.Customers.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Projects.Controllers
{
    [Route("/{CompanyId}/Projects/{action=Index}/{id?}")]
    public class ProjectsController : _Customers_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Create()
        //{
        //    var Project = EnterpriseRepo.Projects.Create("New Project", "N/A");

        //    EnterpriseRepo.SaveChanges();

        //    return Redirect($"/@CompanyId/Projects/{Project.Id}");
        //}

    }
}
