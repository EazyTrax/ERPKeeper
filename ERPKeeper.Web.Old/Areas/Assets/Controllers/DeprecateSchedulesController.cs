using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Assets.Controllers
{
    public class DeprecateSchedulesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }


        public ActionResult Home(Guid? id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult UnPost()
        {
            Organization.FixedAssetDreprecateSchedules.UnPost();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Post()
        {
            Organization.FixedAssetDreprecateSchedules.PostLedger();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public PartialViewResult PartialGridView(Guid? id)
        {
            ViewBag.Id = id;
            Organization.FixedAssetDreprecateSchedules.Clean();

            if (id == null)
                return PartialView(Organization.FixedAssetDreprecateSchedules.GetAll());

            var fiscalYear = Organization.FiscalYears.Find((Guid)id);
            var schedules = Organization.FixedAssets.GetDeprecateSchedule(fiscalYear.EndDate).ToList();
            var endDate = fiscalYear.EndDate;
            var fixedAssets = Organization.FixedAssets.Query().Where(a => a.StartDeprecationDate <= endDate).ToList();


            return PartialView(schedules);

        }
    }
}