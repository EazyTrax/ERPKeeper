using ERPKeeperCore.Enterprise.DBContext;
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


        public Guid AssetId => Guid.Parse(RouteData.Values["AssetId"].ToString());

        public IActionResult Index(Guid AssetId)
        {
            var Asset = Organization.Assets.Find(AssetId);
            return View(Asset);
        }

        public IActionResult CreateSchedules(Guid AssetId)
        {
            var asset = Organization.Assets.Find(AssetId);

            if (asset.AssetType.DeprecatedAble)
            {
                asset.CreateDepreciationSchedule();
                Organization.SaveChanges();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Post(Guid AssetId)
        {
            var asset = Organization.Assets.Find(AssetId);
            asset.PostToTransaction();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult UnPost(Guid AssetId)
        {
            var asset = Organization.Assets.Find(AssetId);
            asset.Transaction.ClearLedger();
            asset.IsPosted = false;
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        [HttpPost]
        public IActionResult Update(Guid AssetId, ERPKeeperCore.Enterprise.Models.Assets.Asset model)
        {
            var Asset = Organization.Assets.Find(AssetId);

            if (Asset != null && !Asset.IsPosted)
            {
                Asset.Name = model.Name;
                //Asset.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////Asset.CreatedDate = model.CreatedDate;
                Asset.PurchaseDate = model.PurchaseDate;
                //Asset.CloseDate = model.CloseDate;

                //Asset.ProjectUid = model.ProjectUid;
                Asset.Memo = model.Memo;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
