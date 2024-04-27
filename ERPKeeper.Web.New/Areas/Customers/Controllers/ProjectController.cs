using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/{area}/Projects/{ProjectId:Guid}/{action=Index}/{id?}")]
    public class ProjectController : Base_CustomersController
    {
        public IActionResult Index(Guid ProjectId)
        {
            var Project = EnterpriseRepo.Projects.Find(ProjectId);
            return View(Project);
        }

        public IActionResult Close(Guid ProjectId)
        {
            var Project = EnterpriseRepo.Projects.Find(ProjectId);
            Project.ChangeStatus(Enterprise.Models.Projects.Enums.ProjectStatus.Close);

            EnterpriseRepo.SaveChanges();

            return View(Project);
        }

        public IActionResult Open(Guid ProjectId)
        {
            var Project = EnterpriseRepo.Projects.Find(ProjectId);
            Project.ChangeStatus(Enterprise.Models.Projects.Enums.ProjectStatus.Open);
            EnterpriseRepo.SaveChanges();
            return View(Project);
        }

        [HttpPost]
        public IActionResult Update(Guid ProjectId, ERPKeeperCore.Enterprise.Models.Projects.Project model)
        {
            var Project = EnterpriseRepo.Projects.Find(ProjectId);

            if (Project != null)
            {
                Project.Name = model.Name;
                Project.Detail = model.Detail;

                EnterpriseRepo.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
