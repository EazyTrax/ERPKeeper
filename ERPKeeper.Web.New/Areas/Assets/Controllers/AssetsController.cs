using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Assets.Controllers
{

    public class AssetsController : Base_AssetsController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var task = EnterpriseRepo.Tasks.CreateDefault();
            task.ResponsibleMemberUid = CurrentMakerId;
            EnterpriseRepo.SaveChanges();

            return Redirect($"/Assets/Tasks/{task.Uid}");
        }

    }
}
