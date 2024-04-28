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
    [Route("/{CompanyId}/{area}/Transactions/{TransactionId:Guid}/{action=Index}")]
    public class TransactionController : AccountingBaseController
    {
        public Guid TransactionId => Guid.Parse(HttpContext.GetRouteData().Values["TransactionId"].ToString());


        public IActionResult Index()
        {
            var model = EnterpriseRepo.ErpCOREDBContext.TransactionLedgers.Find(TransactionId);
            return View(model);
        }


    }
}
