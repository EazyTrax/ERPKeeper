using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.New.API.Financials.FundTransfer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERPKeeperCore.Enterprise.Models.Financial;

namespace ERPKeeperCore.Web.New.API.Financials.LiabilityPayment
{
    public class PayFromAccountsController : API_Financials_LiabilityPayment_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.LiabilityPaymentPayFromAccounts
                .Where(m => m.LiabilityPaymentId == Id)
                .Include(m => m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Financial.LiabilityPaymentPayFromAccount();
            JsonConvert.PopulateObject(values, model);
            model.LiabilityPaymentId = Id;

            Organization.ErpCOREDBContext.LiabilityPaymentPayFromAccounts.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.LiabilityPaymentPayFromAccounts.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.LiabilityPaymentPayFromAccounts.Find(key);
            Organization.ErpCOREDBContext.LiabilityPaymentPayFromAccounts.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
