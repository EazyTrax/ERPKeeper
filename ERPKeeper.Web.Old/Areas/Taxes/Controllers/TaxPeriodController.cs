using ERPKeeper.Node.Models.Taxes.Enums;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Taxes.Controllers
{
    public class TaxPeriodController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.TaxCodes = Organization.TaxCodes.ListAll();

        }

        public ActionResult Home(Guid id) => View(Organization.TaxPeriods.Find(id));



        public ActionResult Documents(Guid id)
        {
            ViewBag.id = id;
            var taxPeriod = Organization.TaxPeriods.Find(id);
            return View(taxPeriod);
        }

        public ActionResult Ledger(Guid id)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            return View(taxPeriod);
        }



        public ActionResult RemoveCommercial(Guid id, Guid transactionUid)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);

            var commercial = taxPeriod.Commercials
                .Where(c => c.Uid == transactionUid)
                .First();

            taxPeriod.Commercials.Remove(commercial);
            taxPeriod.ReCalculate();
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnAssignCommercial(Guid id)
        {
            ViewBag.id = id;
            var taxPeriod = Organization.TaxPeriods.Find(id);
            return View(taxPeriod);
        }

        public PartialViewResult PartialGridViewUnAssignCommercial(Guid id)
        {
            ViewBag.id = id;
            var taxPeriod = Organization.TaxPeriods.Find(id);
            var unAssignCommercials = Organization.TaxPeriods.GetUnassignCommercial(taxPeriod);

            return PartialView(unAssignCommercials);
        }

        public ActionResult Commercial(Guid id, TaxDirection? taxDirection)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            ViewBag.taxDirection = taxDirection ?? TaxDirection.Input;
            return View(taxPeriod);
        }

        public PartialViewResult PartialGridViewCommercial(Guid id, TaxDirection taxDirection)
        {
            ViewBag.TaxPeriodUid = id;
            var taxPeriod = Organization.TaxPeriods.Find(id);

            List<Commercial> commercialTaxes = taxPeriod.GetCommercials(taxDirection);
            return PartialView(commercialTaxes);
        }


        public ActionResult Save(ERPKeeper.Node.Models.Taxes.TaxPeriod taxPeriod)
        {
            taxPeriod = Organization.TaxPeriods.Save(taxPeriod);
            return RedirectToAction("Home", new { id = taxPeriod.Uid });
        }

        [HttpPost]
        public ActionResult AssignCommercial(Guid id, string selectedUids)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            var count = Organization.TaxPeriods.AssignCommercialTaxs(taxPeriod, selectedUids);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult ExportInputTax(Guid id)
        {
            Organization.Commercials.UpdateAddresses();

            var taxPeriod = Organization.TaxPeriods.Find(id);
            var report = taxPeriod.getInputTaxReport(Organization);


            ViewBag.Report = report;
            return View("Export", taxPeriod);
        }
        public ActionResult ExportOutputTax(Guid id)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            var report = taxPeriod.getOutputTaxReport(Organization);

            ViewBag.Report = report;
            return View("Export", taxPeriod);
        }


        public ActionResult AutoAssignCommercial(Guid id)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            Organization.TaxPeriods.AutoAssignCommercial(taxPeriod);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        [Authorize.ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Admin)]
        public ActionResult Delete(Guid id)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            Organization.TaxPeriods.Delete(taxPeriod);

            return RedirectToAction("Home", "TaxPeriods");
        }


        public ActionResult Update(Guid id)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            taxPeriod.ReCalculate();
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult PostLedger(Guid id)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            Organization.TaxPeriods.PostLedger(taxPeriod);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        public ActionResult UnPost(Guid id)
        {
            var taxPeriod = Organization.TaxPeriods.Find(id);
            Organization.TaxPeriods.UnPost(taxPeriod);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        //public ActionResult NewTaxPayment(Guid id, string reference, decimal amountPayment)
        //{
        //    var taxPeriod = organization.taxPeriods.Find(id);
        //    var taxPayableAccount = taxPeriod.TaxCode.CloseToAccount;

        //    var newTaxPayment = taxPaymentsDal.Create(taxPayableAccount, reference, amountPayment, taxPeriod.Date);


        //    return RedirectToAction("Home", "TaxPayment", new { id = newTaxPayment.Uid });
        //}



    }
}