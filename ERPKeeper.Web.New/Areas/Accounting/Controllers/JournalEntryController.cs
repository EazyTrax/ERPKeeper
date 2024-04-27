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
    [Route("/{CompanyId}/{area}/JournalEntries/{JournalEntryId:Guid}/{action=Index}")]
    public class JournalEntryController : AccountingBaseController
    {
        public Guid JournalEntryId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryId"].ToString());


        public IActionResult Index()
        {
            var model = EnterpriseRepo.JournalEntries.Find(JournalEntryId);
            return View(model);
        }

        public IActionResult UpdateBalance()
        {
            var model = EnterpriseRepo.JournalEntries.Find(JournalEntryId);
            model.UpdateBalance();
            EnterpriseRepo.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult UnPost()
        {
            var model = EnterpriseRepo.JournalEntries.Find(JournalEntryId);
            EnterpriseRepo.JournalEntries.UnPost(model);
            EnterpriseRepo.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Clone()
        {
            var model = EnterpriseRepo.JournalEntries.Find(JournalEntryId);
            var newModel = new JournalEntry()
            {
                Date = DateTime.Today,
                Memo = model.Memo,
                JournalEntryTypeId = model.JournalEntryTypeGuid,
                JournalEntryItems = new HashSet<JournalEntryItem>()
            };
            EnterpriseRepo.ErpCOREDBContext.JournalEntries.Add(newModel);

            model.Items.ToList().ForEach(item =>
            {
                newModel.AddAcount(item.AccountUid, item.Debit, item.Credit);
            });
            EnterpriseRepo.SaveChanges();

            return Redirect($"/{CompanyId}/Accounting/JournalEntries/{model.Uid}/");
        }
    }
}
