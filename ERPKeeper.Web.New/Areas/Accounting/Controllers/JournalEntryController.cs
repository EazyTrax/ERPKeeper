using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{
    [Route("/Accounting/JournalEntries/{JournalEntryId:Guid}/{action=Index}")]
    public class JournalEntryController : AccountingBaseController
    {
        public Guid JournalEntryId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryId"].ToString());


        public IActionResult Index()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            model.UpdateBalance();
            Organization.SaveChanges();
            return View(model);
        }

        public IActionResult Refresh()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            model.UpdateBalance();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult UnPost()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            Organization.JournalEntries.UnPost(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Post()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            Organization.JournalEntries.UnPost(model);
            Organization.SaveChanges();

            model.PostToTransaction();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Delete()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            Organization.JournalEntries.UnPost(model);
            Organization.SaveChanges();

            model.RemoveAllItems();
            Organization.ErpCOREDBContext.JournalEntries.Remove(model);
            Organization.SaveChanges();

            return Redirect($"/Accounting/JournalEntries");
        }



        public IActionResult Clone()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            var newModel = new JournalEntry()
            {
                Date = DateTime.Today,
                Memo = model.Memo,
                JournalEntryTypeId = model.JournalEntryTypeId,
                JournalEntryItems = new HashSet<JournalEntryItem>()
            };
            Organization.ErpCOREDBContext.JournalEntries.Add(newModel);

            model.JournalEntryItems.ToList().ForEach(item =>
            {
                newModel.AddAcount(item.AccountId, item.Debit, item.Credit);
            });
            Organization.SaveChanges();

            return Redirect($"/Accounting/Transactions/{model.Id}/");
        }
    }
}
