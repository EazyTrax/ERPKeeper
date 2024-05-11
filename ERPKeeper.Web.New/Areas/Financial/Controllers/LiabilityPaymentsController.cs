using ERPKeeperCore.Web.Areas.Financial.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Enterprise.Models.Financial;

namespace ERPKeeperCore.Web.Areas.Financial.Controllers
{
    public class LiabilityPaymentsController : Financial_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New([FromQuery] Guid accountId, [FromQuery] decimal amount, [FromQuery] DateTime date)
        {
            var newLiabilityPayment = new LiabilityPayment()
            {
                Date = date,
                Amount = amount,
                LiabilityAccount = OrganizationCore.ChartOfAccount.Find(accountId),
                LiabilityAccountId = accountId,
                Status = Enterprise.Models.Financial.Enums.LiabilityPaymentStatus.Draft,
            };

            OrganizationCore.ErpCOREDBContext.LiabilityPayments.Add(newLiabilityPayment);
            OrganizationCore.SaveChanges();

            newLiabilityPayment.LiabilityPaymentPayFromAccounts.Add(new LiabilityPaymentPayFromAccount()
            {
                Amount = amount,
                Account = OrganizationCore.SystemAccounts.Cash,
            });

            OrganizationCore.SaveChanges();

            return Redirect($"/{CompanyId}/Financial/LiabilityPayments/{newLiabilityPayment.Id}");

        }


    }
}
