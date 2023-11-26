using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    [RouteArea("Accounting")]
    [RoutePrefix("JournalEntryTypes")]
    [Route("{action}/{id?}")]
    public class JournalEntryTypesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home()
        {
            var journalEntryTypes = Organization.JournalEntryTypes.GetAll();
            return View(journalEntryTypes);
        }

        public PartialViewResult _Preview()
        {
            var journalEntryTypes = Organization.JournalEntryTypes.GetAll();
            return PartialView(journalEntryTypes);
        }
    }
}