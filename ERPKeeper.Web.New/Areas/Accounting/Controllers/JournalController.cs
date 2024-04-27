using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/{area}/Journals/{JournalId:Guid}/{action=Index}")]
    public class JournalController : AccountingBaseController
    {
        public Guid JournalId => Guid.Parse(HttpContext.GetRouteData().Values["JournalId"].ToString());


        public IActionResult Index()
        {
            var model = EnterpriseRepo.ErpCOREDBContext.TransactionLedgers.Find(JournalId);
            return View(model);
        }


    }
}
