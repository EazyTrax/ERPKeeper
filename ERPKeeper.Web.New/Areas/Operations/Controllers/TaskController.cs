using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Operations.Controllers
{
    [Route("/{CompanyId}/{area}/Tasks/{TaskId:Guid}/{action=Index}/{id?}")]
    public class TaskController : Base_OperationsController
    {
        public IActionResult Index(Guid taskId)
        {
            var task = Organization.Tasks.Find(taskId);
            return View(task);
        }

        public IActionResult Close(Guid taskId)
        {
            var task = Organization.Tasks.Find(taskId);
            task.ChangeStatus(Node.Models.Tasks.Enums.TaskStatus.Close);
            Organization.SaveChanges();

            return View(task);
        }

        public IActionResult Open(Guid taskId)
        {
            var task = Organization.Tasks.Find(taskId);
            task.ChangeStatus(Node.Models.Tasks.Enums.TaskStatus.Open);
            Organization.SaveChanges();
            return View(task);
        }

        [HttpPost]
        public IActionResult Update(Guid taskId, ERPKeeper.Node.Models.Tasks.Task model)
        {
            var task = Organization.Tasks.Find(taskId);

            if (task != null)
            {
                task.Title = model.Title;
                task.ResponsibleMemberUid = model.ResponsibleMemberUid;
                //task.CreatedDate = model.CreatedDate;
                task.DueDate = model.DueDate;
                task.CloseDate = model.CloseDate;

                task.ProjectUid = model.ProjectUid;
                task.Detail = model.Detail;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
