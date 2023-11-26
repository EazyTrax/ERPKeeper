using ERPKeeper.Node.Models.Taxes;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Taxes.Controllers
{
    public class IncomeTaxController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.IncomeTaxes.Find(id));


        public ActionResult Save(IncomeTax incomeTax)
        {
            var existIncomeTax = Organization.IncomeTaxes.Find(incomeTax.Uid);

            existIncomeTax.Update(incomeTax);
            existIncomeTax.FiscalYear = Organization.FiscalYears.Find(incomeTax.TrDate);

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Delete(Guid id)
        {
            var result = Organization.IncomeTaxes.Delete(id);
            return RedirectToAction("Home", "IncomeTaxes");
        }

        public ActionResult Edit(Guid id)
        {
            var result = Organization.IncomeTaxes.Edit(id);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}