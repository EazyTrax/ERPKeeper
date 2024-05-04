using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Assets.Controllers
{
    [Route("/{CompanyId}/Assets/AssetTypes/{AssetTypeId:Guid}/{action=Index}/{id?}")]
    public class AssetTypeController : Base_AssetsController
    {
        public IActionResult Index(Guid AssetTypeId)
        {
            var AssetType = OrganizationCore.AssetTypes.Find(AssetTypeId);
            return View(AssetType);
        }

     
        [HttpPost]
        public IActionResult Update(Guid AssetTypeId, ERPKeeperCore.Enterprise.Models.Assets.AssetType model)
        {
            var AssetType = OrganizationCore.AssetTypes.Find(AssetTypeId);

            if (AssetType != null)
            {
                //AssetType.Title = model.Title;
                //AssetType.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////AssetType.CreatedDate = model.CreatedDate;
                //AssetType.DueDate = model.DueDate;
                //AssetType.CloseDate = model.CloseDate;

                //AssetType.ProjectUid = model.ProjectUid;
                //AssetType.Detail = model.Detail;

                OrganizationCore.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
