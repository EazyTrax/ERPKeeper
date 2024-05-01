using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Accounting.FiscalYear
{
    public class JournalsController : FiscalYearBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var fiscalYear = Organization.ErpCOREDBContext.FiscalYears.First(a => a.Id == FiscalYearId);

            var returnModel = Organization.ErpCOREDBContext
                .TransactionLedgers;
            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.TransactionLedgers.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

    
    }
}
