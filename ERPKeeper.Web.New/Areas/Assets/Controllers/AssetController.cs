using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeper.Web.New.Areas.Assets.Controllers
{
    [Route("/{CompanyId}/{area}/Assets/{AssetId:Guid}/{action=Index}/{id?}")]
    public class AssetController : Base_AssetsController
    {
        public IActionResult Index(Guid AssetId)
        {
            var Asset = Organization.FixedAssets.Find(AssetId);
            return View(Asset);
        }

      
        [HttpPost]
        public IActionResult Update(Guid AssetId, ERPKeeper.Node.Models.Assets.FixedAsset model)
        {
            var Asset = Organization.FixedAssets.Find(AssetId);

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
