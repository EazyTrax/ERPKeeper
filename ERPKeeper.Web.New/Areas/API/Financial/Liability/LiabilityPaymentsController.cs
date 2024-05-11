using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.New.API.Financials;
using ERPKeeperCore.Web.New.API.Financials.FundTransfer;
using ERPKeeperCore.Web.New.API.Financials.Liability;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Financials.Liability
{
    public class LiabilityPaymentsController : API_Financials_Liability_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.LiabilityPayments
                .Where(m => m.LiabilityAccountId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }




    }
}
