using ERPKeeper.Node.Models.Projects;
using ERPKeeper.Node.Models.Projects.Enums;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Projects.Controllers
{
    public class ProjectsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        // GET: Documents/Home
        public ActionResult Home(ProjectStatus projectStatus = ProjectStatus.Active)
        {
            ViewBag.ProjectStatus = projectStatus;
            return View(Organization.Projects.GetListByStatus(projectStatus));
        }


        public PartialViewResult PartialGridViewProjects(ProjectStatus projectStatus = ProjectStatus.Active)
        {
            ViewBag.ProjectStatus = projectStatus;
            return PartialView(Organization.Projects.GetListByStatus(projectStatus));
        }

        public PartialViewResult _TreeViewProjects(ProjectStatus projectStatus = ProjectStatus.Active)
        {
            ViewBag.ProjectStatus = projectStatus;
            return PartialView(Organization.Projects.GetListByStatus(projectStatus));
        }


        public PartialViewResult _New()
        {
            var Project = new ERPKeeper.Node.Models.Projects.NewProjectModel()
            {
                ProfileGuid = CurrentUser.ProfileUid
            };
            return PartialView(Project);
        }

        public ActionResult AddNew(NewProjectModel newProjectModel)
        {
            var newProject = Organization.Projects.CreateNew(newProjectModel);

            return RedirectToAction("Home", "Project", new { id = newProject.Uid });
        }

    }
}