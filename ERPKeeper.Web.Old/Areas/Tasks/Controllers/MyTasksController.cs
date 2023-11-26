using ERPKeeper.Node.Models.Projects;
using ERPKeeper.Node.Models.Tasks.Enums;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Tasks.Controllers
{
    public class MyTasksController : WebFrontEnd.Controllers._DefaultNodeController
    {
        // GET: Documents/Home
        public ActionResult Home(TaskStatus id = TaskStatus.Open)
        {
            var tasks = Organization.Tasks.All()
                .Where(t => t.Status == id)
                .ToList();
            return View(tasks);
        }

        public PartialViewResult _OpenTaskSummary()
        {
            var tasks = Organization.Tasks.All()
                .Where(t => t.Status != TaskStatus.Close)
                .ToList();
            return PartialView(tasks);
        }

        public PartialViewResult PartialGridViewTasks()
        {
            return PartialView(Organization.Tasks.ListAll());
        }

        public PartialViewResult PartialGridViewYoursOpenTasks()
        {
            var tasks = Organization.Tasks.All()
                .Where(t => t.ResponsibleMemberUid == CurrentUser.ProfileUid)
                .Where(t => t.Status == TaskStatus.Open)
                .ToList();
            return PartialView(tasks);
        }

        public PartialViewResult PartialGridViewYoursTasks()
        {
            var tasks = Organization.Tasks.All()
                .Where(t => t.ResponsibleMemberUid == CurrentUser.ProfileUid)
                .ToList();
            return PartialView(tasks);
        }

        public PartialViewResult _NewTask()
        {
            var task = new NewTask()
            {
                ProfileGuid = CurrentUser.ProfileUid
            };
            return PartialView(task);
        }

        public ActionResult AddNewTask(NewTask newTaskModel)
        {
            newTaskModel.CreatorUid = CurrentUser.ProfileUid;
            newTaskModel.ResponsibleUid = CurrentUser.ProfileUid;
            newTaskModel.CreatedDate = DateTime.Today;

            var newTask = Organization.Tasks.CreateNew(newTaskModel);
            return RedirectToAction("Home", "Task", new { id = newTask.Uid });
        }
    }
}