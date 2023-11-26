using ERPKeeper.Node.Models.Assets.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Assets.Controllers
{
    public class FixedAssetsController : WebFrontEnd.Controllers._DefaultNodeController
    {



        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home(FixedAssetStatus? status)
        {
            ViewBag.Status = status;


            return View();
        }

        [ValidateInput(false)]
        public PartialViewResult PartialGridView(FixedAssetStatus? status)
        {
            ViewBag.Status = status;


            if (status != null)
            {
                var fixedAssets = Organization.FixedAssets.Query()
                    .Where(d => d.Status == status)
                    .ToList();
                return PartialView(fixedAssets);
            }
            else
                return PartialView(Organization.FixedAssets.GetListAll());
        }


        public ActionResult New()
        {
            return View();
        }

        public ActionResult CreateSchedule()
        {
            Organization.FixedAssets.CreateDeprecationSchedule();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult SyncTransaction()
        {
            //deprecatedAssetsDal.SyncTransaction();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult ReOrder()
        {
            Organization.FixedAssets.ReOrder();
            Organization.ErpNodeDBContext
                .DeprecateSchedules
                .Where(d => d.FiscalYearUid == null)
                .ToList()
                .ForEach(d => d.FiscalYear = Organization.FiscalYears.Find(d.StartDate));
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UpdateStatus()
        {
            Organization.FixedAssets.UpdateStatus();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Export()
        {


            var Report = new Node.Reports.Assets.Asset()
            {
                DataSource = Organization.FixedAssets.GetListAll(),
                Name = "Assets"
            };
            ViewBag.Report = Report;
            return View("Export");
        }

    }
}