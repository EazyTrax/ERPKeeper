using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Accounting
{
    public class FiscalYearsController : API_Accounting_BaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.FiscalYears
                .Include(a => a.FiscalYearAccountBalances)
                .ThenInclude(a => a.Account)
                .ToList();
                
            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Accounting.FiscalYear();
            JsonConvert.PopulateObject(values, model);
            
            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpCOREDBContext.FiscalYears.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.FiscalYears.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.FiscalYears.First(a => a.Id == key);
            Organization.ErpCOREDBContext.FiscalYears.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
