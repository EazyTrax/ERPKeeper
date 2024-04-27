using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;
using ERPKeeperCore.Enterprise.DAL;
using ERPKeeperCore.Enterprise.DAL.Accounting;

namespace ERPKeeperCore.Web.Area.Accounting.Views.Shared.Components
{

    public class _Journal_Accounting : ViewComponent
    {
        public IViewComponentResult Invoke(string companyId, Guid journalId)
        {
            var organization = new Organization(companyId);
            var joural = organization.ErpNodeDBContext.TransactionLedgers.Find(journalId);

            return View(@"/Areas/Accounting/Views/Shared/Components/_Journal_Accounting/_Ledgers_Summary_Grid.cshtml", joural);
        }
    }
}
