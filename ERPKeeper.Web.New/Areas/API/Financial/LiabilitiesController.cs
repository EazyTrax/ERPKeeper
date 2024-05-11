using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.New.API.Financials;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Financials
{
    public class LiabilitiesController : API_Financials_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Accounts.Where(a => a.Type == Enterprise.Models.Accounting.Enums.AccountTypes.Liability)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
