using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Assets.Controllers
{
    [Route("/{CompanyId}/Assets/ObsoleteAssets/{AssetId:Guid}/{action=Index}/{id?}")]
    public class ObsoleteAssetController : Base_AssetsController
    {
        public IActionResult Index(Guid AssetId)
        {
            var Asset = Organization.ObsoleteAssets.Find(AssetId);
            return View(Asset);
        }

      
       
    }
}
