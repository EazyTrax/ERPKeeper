using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Assets.Controllers
{
    [Route("/{CompanyId}/Assets/Assets/{AssetId:Guid}/{action=Index}/{id?}")]
    public class AssetController : Base_AssetsController
    {
        public IActionResult Index(Guid AssetId)
        {
            var Asset = Organization.Assets.Find(AssetId);
            return View(Asset);
        }

      
        [HttpPost]
        public IActionResult Update(Guid AssetId, ERPKeeperCore.Enterprise.Models.Assets.Asset model)
        {
            var Asset = Organization.Assets.Find(AssetId);

            if (Asset != null)
            {
                Asset.Name = model.Name;
                //Asset.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////Asset.CreatedDate = model.CreatedDate;
                //Asset.DueDate = model.DueDate;
                //Asset.CloseDate = model.CloseDate;

                //Asset.ProjectUid = model.ProjectUid;
                //Asset.Detail = model.Detail;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
