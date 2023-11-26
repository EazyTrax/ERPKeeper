using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Investors.Controllers
{
    public class InvestorController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home()
        {
            return View();
        }



        public ActionResult NewInvestment(Guid id)
        {
            var newInvestment = Organization.CapitalActivities.CreateNew(id);
            return RedirectToAction("Home", "CapitalActivity", new { id = newInvestment.Uid });
        }


        public ActionResult Home(Guid id)
        {
            var investor = Organization.Investors.Find(id);
            return View(investor);
        }

        public ActionResult Add(Guid id)
        {
            var profile = Organization.Profiles.Find(id);
            var newInvestor = Organization.Investors.Create(profile);

            return RedirectToAction("Home", "Investor", new { id = newInvestor.ProfileUid, Area = "Investors" });
        }

        public ActionResult Delete(Guid id)
        {
            Organization.Investors.Delete(id);
            return RedirectToAction("Home", "Investors");
        }

        public ActionResult CapitalActivities(Guid id)
        {
            var investor = Organization.Investors.Find(id);
            return View(investor);
        }

        public PartialViewResult PartialGridViewCapitalActivities(Guid id)
        {
            var investor = Organization.Investors.Find(id);
            return PartialView(investor.CapitalActivities.ToList());
        }


    }
}