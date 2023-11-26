using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Operations.Controllers
{

    public class TasksController : Base_OperationsController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var task = Organization.Tasks.CreateDefault();
            task.ResponsibleMemberUid = CurrentMakerId;
            Organization.SaveChanges();

            return Redirect($"/Operations/Tasks/{task.Uid}");
        }

    }
}
