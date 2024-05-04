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
    [Route("/{CompanyId}/Accounting/JournalEntries/{JournalEntryId:Guid}/{action=Index}")]
    public class JournalEntryController : AccountingBaseController
    {
        public Guid JournalEntryId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryId"].ToString());


        public IActionResult Index()
        {
            var model = OrganizationCore.JournalEntries.Find(JournalEntryId);
            return View(model);
        }

        public IActionResult UpdateBalance()
        {
            var model = OrganizationCore.JournalEntries.Find(JournalEntryId);
            model.UpdateBalance();
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult UnPost()
        {
            var model = OrganizationCore.JournalEntries.Find(JournalEntryId);
            OrganizationCore.JournalEntries.UnPost(model);
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Clone()
        {
            var model = OrganizationCore.JournalEntries.Find(JournalEntryId);
            var newModel = new JournalEntry()
            {
                Date = DateTime.Today,
                Memo = model.Memo,
                JournalEntryTypeId = model.JournalEntryTypeId,
                JournalEntryItems = new HashSet<JournalEntryItem>()
            };
            OrganizationCore.ErpCOREDBContext.JournalEntries.Add(newModel);

            model.JournalEntryItems.ToList().ForEach(item =>
            {
                newModel.AddAcount(item.AccountId, item.Debit, item.Credit);
            });
            OrganizationCore.SaveChanges();

            return Redirect($"/{CompanyId}/Accounting/Transactions/{model.Id}/");
        }
    }
}
