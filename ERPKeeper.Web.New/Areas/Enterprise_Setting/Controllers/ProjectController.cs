
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Enterprise_Setting.Controllers
{
    public class ProjectController : _Enterprise_Setting_BaseController
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

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
