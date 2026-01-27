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
    public class JournalEntriesController : FiscalYearBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var fiscalYear = Organization.ErpCOREDBContext.FiscalYears.First(a => a.Id == FiscalYearId);

            var returnModel = Organization.ErpCOREDBContext
                .JournalEntries
                .Where(a => a.Date >= fiscalYear.StartDate && a.Date < fiscalYear.EndDate.AddDays(1));


            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntry();
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            //  model.Status = JournalEntryStatus.Draft;
            Organization.ErpCOREDBContext.JournalEntries.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }



    }
}
