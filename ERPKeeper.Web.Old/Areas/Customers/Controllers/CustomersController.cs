using ERPKeeper.Node.Models.Profiles;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    public class CustomersController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(ProfileQueryType id = ProfileQueryType.Active)
        {
            ViewBag.Type = id;
            return View();
        }

        public ActionResult Tree(ProfileQueryType id = ProfileQueryType.Active)
        {
            ViewBag.Type = id;
            return View();
        }

        public ActionResult DashBoard() => View();


        public ActionResult Top() => View();

        public ActionResult UpdateAmount()
        {
            Organization.Customers.UpdateSalesBalance();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public PartialViewResult _Tree(ProfileQueryType id = ProfileQueryType.Active)
        {
            var profiles = Organization.Customers.GetByStatus(id);
            return PartialView(profiles);
        }

        public PartialViewResult PartialGridView(ProfileQueryType id = ProfileQueryType.Active)
        {
            ViewBag.id = id;
            var profiles = Organization.Customers.GetByStatus(id);
            return PartialView(profiles);
        }

        public PartialViewResult _TopGridView(int amount = 15)
        {
            var profiles = Organization.Customers.GetTopSales(amount);
            return PartialView(profiles);
        }

        public PartialViewResult _Chart()
        {
            var profiles = Organization.Customers.GetTopSales(10);
            return PartialView(profiles);
        }
        public ActionResult Add(Guid id)
        {
            var profile = Organization.Profiles.Find(id);
            var newCustomer = Organization.Customers.Create(profile);
            return RedirectToAction("Home", "Customer", new { id = newCustomer.ProfileUid, Area = "Customers" });
        }

    }
}