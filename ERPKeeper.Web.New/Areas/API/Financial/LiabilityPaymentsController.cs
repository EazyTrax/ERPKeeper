using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.New.API.Financials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Financials
{
    public class LiabilityPaymentsController : API_Financials_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.LiabilityPayments
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Financial.LiabilityPayment();
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            Organization.ErpCOREDBContext.LiabilityPayments.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();


            var cashAccount = Organization.ErpCOREDBContext.DefaultAccounts
                .Include(x => x.Account)
                .FirstOrDefault(x => x.Type == Enterprise.Models.Accounting.Enums.DefaultAccountType.Cash)
                .Account;


            model.LiabilityPaymentPayFromAccounts
                .Add(new ERPKeeperCore.Enterprise.Models.Financial.LiabilityPaymentPayFromAccount()
                {
                    Amount = model.Amount,
                    AccountId = cashAccount.Id,
                });

            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.LiabilityPayments.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

    }
}
