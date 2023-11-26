using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Setting.Controllers
{
    public class ShippingMethodController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();

        public ActionResult Home(Guid id) => View(Organization.PaymentTerms.Find(id));

        public ActionResult Save(ShippingMethod term)
        {
            var existTerm = Organization.ShippingMethods.Find(term.Uid);

            if (existTerm == null)
                existTerm = Organization.ShippingMethods.CreateNew(term);
            else
                existTerm.Update(term);

            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = term.Uid });
        }
    }
}