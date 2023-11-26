using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{

    [RouteArea("Accounting")]
    [RoutePrefix("Ledgers")]
    [Route("{action}/{id?}")]
    public class LedgersController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid? id)
        {
            ViewBag.id = id;
            return View(id);
        }

        public ActionResult ViewByTr(Guid? id)
        {
            ViewBag.id = id;
            return View(id);
        }

        public ActionResult Ledger(Guid? id)
        {
            return RedirectToAction("Home", new { Id = id });
        }


        public PartialViewResult PartialGridViewLedger(Guid? id)
        {
            ViewBag.id = id;
            var generalLedgerItems = Organization.LedgersDal.GetList(id);
            return PartialView(generalLedgerItems);
        }

        public PartialViewResult _SummaryLedger(Guid id)
        {
            var generalLedgerItems = Organization.Journals.Find(id);
            return PartialView(generalLedgerItems);
        }


        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult PostLedger()
        {
            Organization.LedgersDal.PostLedgers();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


    }
}