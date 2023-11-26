using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeper.Web.New.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/{area}/Projects/{ProjectId:Guid}/{action=Index}/{id?}")]
    public class ProjectController : Base_CustomersController
    {
        public IActionResult Index(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            return View(Project);
        }

        public IActionResult Close(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            Project.ChangeStatus(Node.Models.Projects.Enums.ProjectStatus.Close);

            Organization.SaveChanges();

            return View(Project);
        }

        public IActionResult Open(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            Project.ChangeStatus(Node.Models.Projects.Enums.ProjectStatus.Active);
            Organization.SaveChanges();
            return View(Project);
        }

        [HttpPost]
        public IActionResult Update(Guid ProjectId, ERPKeeper.Node.Models.Projects.Project model)
        {
            var Project = Organization.Projects.Find(ProjectId);

            if (Project != null)
            {
                Project.Name = model.Name;
                Project.Detail = model.Detail;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
