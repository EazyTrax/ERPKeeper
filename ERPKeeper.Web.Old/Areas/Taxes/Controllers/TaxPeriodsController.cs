using DevExpress.XtraReports.UI;
using ERPKeeper.Node.Models.Taxes.Enums;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Taxes.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class TaxPeriodsController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(TaxDirection? id)
        {
            ViewBag.TaxDirection = id;
            return View();
        }


        public PartialViewResult PartialGridView() => PartialView(Organization.TaxPeriods.GetList());
        public ActionResult UnAssignCommercial() => View();

        public PartialViewResult PartialGridViewUnAssignCommercial()
        {
            throw new NotImplementedException();
        }

        public ActionResult Create()
        {
            var newTaxPeriod = Organization.TaxPeriods.Create();
            return RedirectToAction("Home", "TaxPeriod", new { id = newTaxPeriod.Uid });
        }

        public ActionResult AutoAssign()
        {
            Organization.TaxPeriods.AutoAssignCommercial();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Update()
        {
            Organization.TaxPeriods.ReCalculate();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult ReOrder()
        {
            Organization.TaxPeriods.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Export()
        {


            Organization.TaxPeriods.GetList().ForEach(tp =>
            {
                var inputTaxReport = tp.getInputTaxReport(Organization);
                inputTaxReport.CreateDocument();
                inputTaxReport.ExportToPdf(@"c:/temp/VAT/Input/" + $"INPUT-{tp.PeriodStartDate.Year}-{tp.PeriodStartDate.Month}.pdf");

                var outputTaxReport = tp.getOutputTaxReport(Organization);
                outputTaxReport.CreateDocument();
                outputTaxReport.ExportToPdf(@"c:/temp/VAT/Output/" + $"OUTPUT-{tp.PeriodStartDate.Year}-{tp.PeriodStartDate.Month}.pdf");

            });



            return Content("Complete");

        }


    }
}