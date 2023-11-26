using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Taxes.Controllers
{
    public class TaxCodesController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }



        public ActionResult Home()
        {
            return View();
        }

        public ActionResult New()
        {
            var _TaxCode = new ERPKeeper.Node.Models.Taxes.TaxCode();
            return View("Home", _TaxCode);
        }

        public PartialViewResult PartialGridViewTaxCodes()
        {
            var TaxCodes = Organization.TaxCodes.ListAll();
            return PartialView(TaxCodes);
        }


    }
}