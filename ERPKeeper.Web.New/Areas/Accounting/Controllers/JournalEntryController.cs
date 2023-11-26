using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/{area}/JournalEntries/{JournalEntryId:Guid}/{action=Index}")]
    public class JournalEntryController : AccountingBaseController
    {
        public Guid JournalEntryId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryId"].ToString());


        public IActionResult Index()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            return View(model);
        }

        public IActionResult UpdateBalance()
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
        public IActionResult Clone()
        {
            var model = Organization.JournalEntries.Find(JournalEntryId);
            var newModel = new JournalEntry()
            {
                TrnDate = DateTime.Today,
                Memo = model.Memo,
                JournalEntryTypeGuid = model.JournalEntryTypeGuid,
                Items = new HashSet<JournalEntryItem>()
            };
            Organization.ErpNodeDBContext.JournalEntries.Add(newModel);

            model.Items.ToList().ForEach(item =>
            {
                newModel.AddAcount(item.AccountUid, item.Debit, item.Credit);
            });
            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Accounting/JournalEntries/{model.Uid}/");
        }
    }
}
