using ERPKeeper.Node.Models.Equity.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Investors.Controllers
{
    public class InvestorsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home(InvestorStatus Status = InvestorStatus.Active)
        {
            ViewBag.Status = Status;
            Organization.Investors.UpdateStockCount();
            return View();
        }

        public PartialViewResult PartialGridViewInvestor(InvestorStatus Status = InvestorStatus.Active)
        {
            return PartialView(Organization.Investors.GetListAll());
        }

        public PartialViewResult _Chart() => PartialView(Organization.Investors.GetListAll());
    }
}