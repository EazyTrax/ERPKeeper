using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Taxes.TaxPeriod;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace ERPKeeperCore.Web.API.Taxes.TaxPeriod
{
    public class PurchasesController : API_Taxes_TaxPeriods_TaxPeriod_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization
                .ErpCOREDBContext
                .Purchases
                .Where(s=>s.TaxPeriodId == TaxPeriodId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
