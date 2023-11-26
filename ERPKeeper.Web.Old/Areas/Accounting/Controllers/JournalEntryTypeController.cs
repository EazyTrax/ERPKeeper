using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    [RouteArea("Accounting")]
    [RoutePrefix("JournalEntryType")]
    [Route("{id}/{action=Home}")]
    public class JournalEntryTypeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home(Guid id)
        {
            var jourmalEntry = Organization.JournalEntryTypes.Find(id);
            return View(jourmalEntry);
        }



        public ActionResult ListEnties(Guid id)
        {
            var jourmalEntry = Organization.JournalEntryTypes.Find(id);
            return View(jourmalEntry);
        }

        public ActionResult Save(ERPKeeper.Node.Models.Accounting.JournalEntryType journalEntryType)
        {
            var existJourmalEntryType = Organization.JournalEntryTypes.Find(journalEntryType.Uid);

            if (existJourmalEntryType != null)
                existJourmalEntryType.Update(journalEntryType);
            else
                Organization.JournalEntryTypes.CreateNew(journalEntryType);

            Organization.SaveChanges();
            return RedirectToAction("Home", "journalEntryType", new { id = journalEntryType.Uid });
        }


        public ActionResult New()
        {
            return View("Home");
        }
    }
}