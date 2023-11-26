using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    public class DashBoardController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        public ActionResult Home()
        {
            return View();
        }


        public ActionResult PostGL()
        {
            Organization.LedgersDal.PostLedgers();
            Organization.ChartOfAccount.UpdateBalance();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        //public ActionResult UnPostGL()
        //{
        //  Organization.LedgersDal.UnPosts();
        //  Organization.Accounting.UpdateBalance();

        //  return Redirect(Request.UrlReferrer.PathAndQuery);
        //}
    }
}