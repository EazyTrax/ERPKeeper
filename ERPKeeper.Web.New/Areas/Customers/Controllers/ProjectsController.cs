using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    public class ProjectsController : Base_CustomersController
    {
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Create()
        //{
        //    var Project = EnterpriseRepo.Projects.Create("New Project", "N/A");
         
        //    EnterpriseRepo.SaveChanges();

        //    return Redirect($"/Customers/Projects/{Project.Id}");
        //}

    }
}
