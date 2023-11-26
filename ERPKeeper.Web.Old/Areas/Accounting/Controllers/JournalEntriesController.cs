using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    [RouteArea("Accounting")]
    [RoutePrefix("JournalEntries")]
    [Route("{action}/{id?}")]
    public class JournalEntriesController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home()
        {
            var journalEntries = Organization.JournalEntries.GetAll();
            return View(journalEntries);
        }

        public ActionResult New(Guid id)
        {
            var newJourmalEntry = Organization.JournalEntries.CreateNew(id);

            return RedirectToAction("Home", new { id = newJourmalEntry.Uid });
        }


        public PartialViewResult PartialGridView()
        {
            var journalEntries = Organization.JournalEntries.GetAll();
            return PartialView(journalEntries);
        }

        public ActionResult ReOrder()
        {
            Organization.JournalEntries.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}