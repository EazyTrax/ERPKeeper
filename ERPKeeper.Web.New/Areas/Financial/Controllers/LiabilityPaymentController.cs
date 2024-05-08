using ERPKeeperCore.Web.Areas.Financial.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Financials.Controllers
{

    [Route("/{CompanyId}/Financial/LiabilityPayments/{TransactionId:Guid}/{action=index}")]
    public class LiabilityPaymentController : Financial_BaseController
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var transcation = OrganizationCore.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);
            return View(transcation);
        }
    }
}
