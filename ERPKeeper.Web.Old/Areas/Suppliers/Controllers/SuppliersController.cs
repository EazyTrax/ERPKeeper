using ERPKeeper.Node.Models.Profiles;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{


    public class SuppliersController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home(ProfileQueryType id = ProfileQueryType.Active)
        {
            ViewBag.Id = id;
            return View();
        }



        public ActionResult DashBoard() => View();

        public ActionResult UpdateAmount()
        {
            Organization.Purchases.UpdatePurchasingBalance();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public PartialViewResult PartialGridView(ProfileQueryType id = ProfileQueryType.Active)
        {
            ViewBag.Id = id;
            var profiles = Organization.Suppliers.GetByType(id);
            return PartialView(profiles);
        }

        public PartialViewResult _TopGridView(ProfileQueryType Type = ProfileQueryType.Active)
        {
            var profiles = Organization.Suppliers
                .GetByType(Type)
                .OrderByDescending(c => c.SumPurchaseBalance)
                .Take(20)
                .Where(c => c.SumPurchaseBalance > 0)
                .ToList();

            return PartialView(profiles);
        }

    }
}