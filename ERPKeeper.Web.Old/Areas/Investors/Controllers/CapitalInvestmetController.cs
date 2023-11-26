using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Equity;
using ERPKeeper.Node.Models.Equity.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Investors.Controllers
{
    public class CapitalActivityController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(Guid id)
        {
            var capitalActivity = Organization.CapitalActivities.Find(id);
            return View(capitalActivity);
        }


        public ActionResult New(CapitalActivityType type, Guid investorUid)
        {
            var equityAccount = Organization.SystemAccounts.GetAccount(SystemAccountType.EquityStock);
            var cashAccount = Organization.SystemAccounts.Cash;

            var newCapitalActivity = new ERPKeeper.Node.Models.Equity.CapitalActivity()
            {
                TrnDate = DateTime.Today,
                InvestorUid = investorUid,
                Type = type,
                EquityAccountGuid = equityAccount.Uid,
                AssetAccountGuid = cashAccount.Uid
            };


            return View("Home", newCapitalActivity);
        }

        public ActionResult Delete(Guid id)
        {
            Organization.CapitalActivities.Delete(id);
            return RedirectToAction("Home", "CapitalActivities", new { });
        }



        [HttpPost]
        public ActionResult Save(CapitalActivity capitalInvestment)
        {
            var model = Organization.CapitalActivities.Save(capitalInvestment);
            return RedirectToAction("Home", new { id = model.Uid });
        }

        public ActionResult PostLedger(Guid id)
        {
            var capitalActivity = Organization.CapitalActivities.Find(id);
            Organization.CapitalActivities.PostLedger(capitalActivity);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnPost(Guid id)
        {
            var capitalActivity = Organization.CapitalActivities.Find(id);
            Organization.CapitalActivities.UnPost(capitalActivity);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


    }
}