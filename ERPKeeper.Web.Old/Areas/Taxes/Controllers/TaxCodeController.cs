using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Taxes.Controllers
{
    public class TaxCodeController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var TaxCode = Organization.TaxCodes.Find(id);
            return View(TaxCode);
        }

        public ActionResult New()
        {
            var newTaxCode = Organization.TaxCodes.CreateNew(null);

            return RedirectToAction("Home", new
            {
                id = newTaxCode.Uid
            });
        }





        public ActionResult Save(ERPKeeper.Node.Models.Taxes.TaxCode TaxCode)
        {
            var existTaxCode = Organization.TaxCodes.Find(TaxCode.Uid);
            existTaxCode.Update(TaxCode);
            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = TaxCode.Uid });
        }


        [Authorize.ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Admin)]
        public ActionResult Delete(Guid id)
        {
            var taxCode = Organization.TaxCodes.Find(id);
            Organization.TaxCodes.Remove(taxCode);

            return RedirectToAction("Home", "TaxCodes");
        }
    }
}
