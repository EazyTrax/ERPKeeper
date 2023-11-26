using ERPKeeper.Node.Models.Tasks.Enums;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Tasks.Controllers
{
    [RouteArea("Tasks")]
    [Route("Task/{id}/{action=Home}")]
    public class TaskController : WebFrontEnd.Controllers._DefaultNodeController
    {

        public ActionResult Home(Guid id)
        {
            var task = Organization.Tasks.Find(id);
            return View(task);
        }


        public ActionResult ChangeResponsible(Guid id, Guid memberUid)
        {
            var task = Organization.Tasks.Find(id);
            task.ResponsibleMemberUid = memberUid;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Close(Guid id)
        {
            var task = Organization.Tasks.Find(id);
            task.SetStatus(TaskStatus.Close);

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UpdateStatus(Guid id, ERPKeeper.Node.Models.Tasks.Enums.TaskStatus newStatus)
        {
            var task = Organization.Tasks.Find(id);
            task.Status = newStatus;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult MakeClose(Guid id)
        {
            var task = Organization.Tasks.Find(id);
            task.ChangeStatus(TaskStatus.Close);

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Delete(Guid id)
        {
            var task = Organization.Tasks.Find(id);
            Organization.Tasks.Remove(task);
            return RedirectToAction("Home", "Tasks", new { });
        }

        public ActionResult Save(ERPKeeper.Node.Models.Tasks.Task task)
        {
            var existTask = Organization.Tasks.Find(task.Uid);

            existTask.Detail = task.Detail;
            existTask.DueDate = task.DueDate;

            if (task.Title.Trim() != string.Empty)
                existTask.Title = task.Title;

            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult SendLineNotify(Guid id)
        {
            var task = Organization.Tasks.Find(id);
            task.SendLineNotification();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}