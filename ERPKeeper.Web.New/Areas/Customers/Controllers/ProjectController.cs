using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/Customers/Projects/{ProjectId:Guid}/{action=Index}/{id?}")]
    public class ProjectController : _Customers_Base_Controller
    {
        public IActionResult Index(Guid ProjectId)
        {
            var Project = OrganizationCore.Projects.Find(ProjectId);

            if (Project == null)
                return NotFound();


            return View(Project);
        }

        public IActionResult Close(Guid ProjectId)
        {
            var Project = OrganizationCore.Projects.Find(ProjectId);
            Project.ChangeStatus(Enterprise.Models.Projects.Enums.ProjectStatus.Close);

            OrganizationCore.SaveChanges();

            return View(Project);
        }

        public IActionResult Open(Guid ProjectId)
        {
            var Project = OrganizationCore.Projects.Find(ProjectId);
            Project.ChangeStatus(Enterprise.Models.Projects.Enums.ProjectStatus.Open);
            OrganizationCore.SaveChanges();
            return View(Project);
        }

        [HttpPost]
        public IActionResult Update(Guid ProjectId, ERPKeeperCore.Enterprise.Models.Projects.Project model)
        {
            var Project = OrganizationCore.Projects.Find(ProjectId);

            if (Project != null)
            {
                Project.Name = model.Name;
                Project.Detail = model.Detail;

                OrganizationCore.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
