using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    public class LiabilityAccountsController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home() => View();

        public PartialViewResult PartialGridView()
        {
            return PartialView("PartialGridView", Organization.ChartOfAccount.LiabilityAccounts);
        }

    }
}