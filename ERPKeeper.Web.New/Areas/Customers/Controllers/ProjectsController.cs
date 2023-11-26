using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeper.Web.New.Areas.Customers.Controllers
{

    public class ProjectsController : Base_CustomersController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var Project = Organization.Projects.Create("New Project", "N/A");
         
            Organization.SaveChanges();

            return Redirect($"/Customers/Projects/{Project.Uid}");
        }

    }
}
