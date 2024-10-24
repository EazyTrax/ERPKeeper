using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Assets.Controllers
{

    public class AssetTypesController : Base_AssetsController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Refresh()
        {
            Organization.AssetTypes.Refresh();
            return View();
        }
    }
}
