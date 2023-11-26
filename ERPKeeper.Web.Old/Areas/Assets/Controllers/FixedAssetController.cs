using ERPKeeper.Node.Models.Assets;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Assets.Controllers
{
    public class FixedAssetController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home(Guid id)
        {
            var model = Organization.FixedAssets.Find(id);
            return View(model);
        }
        public ActionResult DeprecateSchedules(Guid id)
        {
            var model = Organization.FixedAssets.Find(id);
            return View(model);
        }




        public ActionResult New(Guid depreciationBasisGuid)
        {
            var deprecatedAsset = new ERPKeeper.Node.Models.Assets.FixedAsset()
            {
                Uid = Guid.NewGuid(),
                StartDeprecationDate = DateTime.Now,
                SavageValue = 1,
                FixedAssetTypeUid = depreciationBasisGuid,
            };

            return View("Home", deprecatedAsset);
        }
        public ActionResult Delete(Guid id)
        {
            var depAsset = Organization.FixedAssets.Find(id);
            Organization.FixedAssets.Remove(depAsset);
            return RedirectToAction("/Assets/FixedAssets");
        }

        public ActionResult Save(ERPKeeper.Node.Models.Assets.FixedAsset fixedAsset)
        {
            var model = fixedAsset;
            Organization.FixedAssets.Save(fixedAsset);

            return RedirectToAction("Home", "FixedAsset", new { id = model.Uid });
        }


        public ActionResult CreateSchedule(Guid id)
        {
            var depAsset = Organization.FixedAssets.Find(id);
            depAsset.CreateDepreciationSchedule();
            Organization.SaveChanges();



            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnPost(Guid id)
        {
            FixedAsset depAsset = Organization.FixedAssets.Find(id);
            Organization.FixedAssets.UnPost(depAsset);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


       

    }
}