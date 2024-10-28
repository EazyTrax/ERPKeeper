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
    [Route("/{CompanyId}/Projects/{ProjectId:Guid}/{action=Index}/")]
    public class ProjectController : _Projects_Base_Controller
    {
        public IActionResult Index(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);

            if (Project == null)
                return NotFound();

            return View(Project);
        }

        public IActionResult Close(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            Project.ChangeStatus(Enterprise.Models.Projects.Enums.ProjectStatus.Close);

            Organization.SaveChanges();

            return View(Project);
        }

        public IActionResult Open(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            Project.ChangeStatus(Enterprise.Models.Projects.Enums.ProjectStatus.Open);
            Organization.SaveChanges();
            return View(Project);
        }

        [HttpPost]
        public IActionResult Update(Guid ProjectId, Enterprise.Models.Projects.Project model)
        {
            var Project = Organization.Projects.Find(ProjectId);

            if (Project != null)
            {
                Project.Name = model.Name;
                Project.Detail = model.Detail;
                Project.StartDate = Project.StartDate;
                Project.EndDate = Project.EndDate;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

     
        public IActionResult Sales(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            return View(Project);
        }
        public IActionResult SaleQuotes(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            return View(Project);
        }
        public IActionResult Purchases(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            return View(Project);
        }
        public IActionResult PurchaseQuotes(Guid ProjectId)
        {
            var Project = Organization.Projects.Find(ProjectId);
            return View(Project);
        }

    }
}
