using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Contents.Controllers
{
    [Area("Storage")]
    public class DocumentController : DefaultController
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
