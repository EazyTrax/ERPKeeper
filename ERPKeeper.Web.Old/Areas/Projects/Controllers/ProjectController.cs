using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Projects.Controllers
{
    [RouteArea("Projects")]
    [Route("Project/{id}/{action=Home}")]
    public class ProjectController : WebFrontEnd.Controllers._DefaultNodeController
    {
        public ActionResult Home(Guid id)
        {
            var project = Organization.Projects.Find(id);
            return View(project);
        }

        public ActionResult AddItem(Guid id, Guid itemUid, int amount)
        {
            var project = Organization.Projects.Find(id);

            var material = new ERPKeeper.Node.Models.Projects.Material()
            {
                Uid = Guid.NewGuid(),
                ItemUid = itemUid,
                Amount = amount,
                RecordDate = DateTime.Today, 
                ProjectUid = id
            };

            project.Materials.Add(material);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Commercials(Guid id)
        {
            ViewBag.Id = id;
            var project = Organization.Projects.Find(id);
            return View(project);
        }

        public ActionResult Quotes(Guid id)
        {
            ViewBag.Id = id;
            var project = Organization.Projects.Find(id);
            return View(project);
        }


        public PartialViewResult PartialGridViewSaleQuotes(Guid id)
        {
            ViewBag.Id = id;
            var project = Organization.Projects.Find(id);
            return PartialView(project);
        }



        public PartialViewResult PartialGridViewCommercials(Guid id)
        {
            ViewBag.Id = id;
            var project = Organization.Projects.Find(id);
            return PartialView(project);
        }

        public PartialViewResult PartialGridViewTasks(Guid id)
        {
            ViewBag.Id = id;
            var project = Organization.Projects.Find(id);
            return PartialView(project);
        }

        public ActionResult Items(Guid id)
        {
            ViewBag.Id = id;
            var project = Organization.Projects.Find(id);
            return View(project);
        }

        public PartialViewResult Partial_ComboBoxItems() => PartialView(Organization.Items.ListAll());


     
        public ActionResult UpdateStatus(Guid id, ERPKeeper.Node.Models.Projects.Enums.ProjectStatus newStatus)
        {
            var project = Organization.Projects.Find(id);
            if (project != null)
            {
                project.Status = newStatus;
                Organization.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Save(ERPKeeper.Node.Models.Projects.Project project)
        {
            var existProject = Organization.Projects.Find(project.Uid);

            if (existProject != null)
            {
                existProject.Detail = project.Detail;
                existProject.Code = project.Code;
                existProject.ParentGuid = project.ParentGuid;
                existProject.CustomerProfileGuid = project.CustomerProfileGuid;

                if (project.Name.Trim() != string.Empty)
                    existProject.Name = project.Name;

                Organization.SaveChanges();


            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }




    }
}