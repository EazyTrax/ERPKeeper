using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    [RouteArea("Accounting")]
    [RoutePrefix("JournalEntry")]
    [Route("{id}/{action=Home}")]
    public class JournalEntryController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home(Guid id) => View(Organization.JournalEntries.Find(id));

        public ActionResult Ledger(Guid id) => RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });

        public ActionResult Delete(Guid id)
        {
            var jourmalEntry = Organization.JournalEntries.Find(id);

            if (jourmalEntry != null && jourmalEntry.PostStatus != LedgerPostStatus.Locked)
            {
                Organization.JournalEntries.Delete(jourmalEntry);
                return RedirectToAction("Home", "JournalEntries");
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Issue(Guid id)
        {
            var jourmalEntry = Organization.JournalEntries.Find(id);

            jourmalEntry.Status = CommercialStatus.Open;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnPost(Guid id)
        {
            var jourmalEntry = Organization.JournalEntries.Find(id);
            Organization.JournalEntries.UnPost(jourmalEntry);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Refresh(Guid id)
        {
            var jourmalEntry = Organization.JournalEntries.Find(id);

            Organization.JournalEntries.RemoveNull();


            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        public ActionResult PostLedger(Guid id)
        {
            var jourmalEntry = Organization.JournalEntries.Find(id);
            Organization.JournalEntries.PostLedger(jourmalEntry);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

     


        public ActionResult Save(ERPKeeper.Node.Models.Accounting.JournalEntry journalEntry)
        {
            Organization.JournalEntries.Save(journalEntry);

            return RedirectToAction("Home", new { id = journalEntry.Uid });
        }

        public PartialViewResult PartialGridView(Guid id)
        {
            var JourmalEntry = Organization.JournalEntries.Find(id);
            return PartialView(JourmalEntry);
        }

        public PartialViewResult PartialGridViewUpdate(Guid id, ERPKeeper.Node.Models.Accounting.JournalEntryItem Item)
        {
            var journalEntryItem = Organization.JournalEntryItems.Find(Item.JournalEntryItemId);

            if (journalEntryItem != null)
                journalEntryItem.Update(Item);

            Organization.SaveChanges();

            return PartialView("PartialGridView", journalEntryItem.JournalEntry);
        }


        public PartialViewResult _ComboBoxAccount()
        {
            var accounts = Organization.ChartOfAccount.GetAccountsList();
            return PartialView(accounts);
        }

        public ActionResult AddAccount(Guid id, Guid accountUid)
        {
            var JourmalEntry = Organization.JournalEntries.Find(id);

            if (JourmalEntry.Items == null)
                JourmalEntry.Items = new HashSet<ERPKeeper.Node.Models.Accounting.JournalEntryItem>();

            JourmalEntry.AddAcount(accountUid);
            Organization.SaveChanges();


            return RedirectToAction("Home", new { id = id });
        }
    }
}