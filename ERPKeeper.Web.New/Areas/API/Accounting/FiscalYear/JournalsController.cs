using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Accounting.FiscalYear
{
    public class JournalsController : FiscalYearBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var fiscalYear = Organization.ErpNodeDBContext.FiscalYears.First(a => a.Uid == FiscalYearId);

            var returnModel = Organization.ErpNodeDBContext
                .TransactionLedgers
                .Where(j =>
                    DbFunctions.TruncateTime(j.TrnDate) >= DbFunctions.TruncateTime(fiscalYear.StartDate) &&
                    DbFunctions.TruncateTime(j.TrnDate) <= DbFunctions.TruncateTime(fiscalYear.EndDate)
                )
                //.Where(m=>m.FiscalYearUid == FiscalYearId)
                ;
            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpNodeDBContext.TransactionLedgers.First(a => a.Uid == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpNodeDBContext.SaveChanges();
            return Ok();
        }

    
    }
}
