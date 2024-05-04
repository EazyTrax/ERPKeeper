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
    [Route("/{CompanyId}/Accounting/Transactions/{TransactionId:Guid}/{action=Index}")]
    public class TransactionController : AccountingBaseController
    {
        public Guid TransactionId => Guid.Parse(HttpContext.GetRouteData().Values["TransactionId"].ToString());


        public IActionResult Index()
        {
            var model = OrganizationCore.ErpCOREDBContext.Transactions.Find(TransactionId);
            return View(model);
        }


    }
}
