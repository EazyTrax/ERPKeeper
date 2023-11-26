using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Setting.Controllers
{
    public class ShippingTermController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);


        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Home(Guid id) => View(Organization.ShippingTerms.Find(id));

        public ActionResult Save(ShippingTerm term)
        {
            var existTerm = Organization.ShippingTerms.Find(term.Uid);

            if (existTerm == null)
                existTerm = Organization.ShippingTerms.CreateNew(term);
            else
                existTerm.Update(term);
            Organization.SaveChanges();

            return RedirectToAction("Home", new
            {
                id = existTerm.Uid
            });
        }
    }
}