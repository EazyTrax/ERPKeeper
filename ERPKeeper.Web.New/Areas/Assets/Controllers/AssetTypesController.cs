using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Assets.Controllers
{

    public class AssetTypesController : Base_AssetsController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Refresh()
        {
            Organization.FixedAssetTypes.Refresh();
            return View();
        }
    }
}
