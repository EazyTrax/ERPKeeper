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
                LiabilityAccount = Organization.ChartOfAccount.Find(accountId),
                LiabilityAccountId = accountId,
                Status = Enterprise.Models.Financial.Enums.LiabilityPaymentStatus.Draft,
            };

            Organization.ErpCOREDBContext.LiabilityPayments.Add(newLiabilityPayment);
            Organization.SaveChanges();

            newLiabilityPayment.LiabilityPaymentPayFromAccounts.Add(new LiabilityPaymentPayFromAccount()
            {
                Amount = amount,
                Account = Organization.SystemAccounts.Cash,
            });

            Organization.SaveChanges();

            return Redirect($"/Financial/LiabilityPayments/{newLiabilityPayment.Id}");

        }


    }
}
