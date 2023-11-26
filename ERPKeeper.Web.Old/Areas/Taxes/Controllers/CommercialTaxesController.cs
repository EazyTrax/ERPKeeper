using ERPKeeper.Node.Models.Taxes.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Taxes.Controllers
{
    public class CommercialTaxesController : WebFrontEnd.Controllers._DefaultNodeController
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

        public ActionResult Assigable() => View();
        public ActionResult NonRecovery() => View();

        public PartialViewResult PartialGridViewCommercialTaxes(TaxDirection? id)
        {
            bool recoveryCommercialTaxOnly = true;
            var comTaxes = Organization.CommercialTaxes.ListCommercialByDirection(id, recoveryCommercialTaxOnly);
            return PartialView(comTaxes);
        }

        public PartialViewResult PartialGridViewAssigableCommercials()
        {
            var assignAbleCommercials = Organization.CommercialTaxes.GetAssignAbleCommercial();
            return PartialView(assignAbleCommercials);
        }

        public PartialViewResult PartialGridViewNonRecoveryCommercialTaxes()
        {
            var nonRecoveryCommercials = Organization.CommercialTaxes.GetNonRecoveryCommercial();
            return PartialView(nonRecoveryCommercials);
        }
    }
}