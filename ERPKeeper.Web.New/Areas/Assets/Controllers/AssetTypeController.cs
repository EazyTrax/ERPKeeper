using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeper.Web.New.Areas.Assets.Controllers
{
    [Route("/{CompanyId}/{area}/AssetTypes/{AssetTypeId:Guid}/{action=Index}/{id?}")]
    public class AssetTypeController : Base_AssetsController
    {
        public IActionResult Index(Guid AssetTypeId)
        {
            var AssetType = Organization.FixedAssetTypes.Find(AssetTypeId);
            return View(AssetType);
        }

     
        [HttpPost]
        public IActionResult Update(Guid AssetTypeId, ERPKeeper.Node.Models.Assets.FixedAssetType model)
        {
            var AssetType = Organization.FixedAssetTypes.Find(AssetTypeId);

            if (AssetType != null)
            {
                //AssetType.Title = model.Title;
                //AssetType.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////AssetType.CreatedDate = model.CreatedDate;
                //AssetType.DueDate = model.DueDate;
                //AssetType.CloseDate = model.CloseDate;

                //AssetType.ProjectUid = model.ProjectUid;
                //AssetType.Detail = model.Detail;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
