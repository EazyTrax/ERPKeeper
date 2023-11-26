using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeper.Node.Models.Accounting.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Accounting
{
    public class SystemAccountsController : AccountingBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.SystemAccounts;
            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeper.Node.Models.Accounting.DefaultAccountItem();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpNodeDBContext.SystemAccounts.Add(model);
            Organization.ErpNodeDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(SystemAccountType key, string values)
        {
            var model = Organization.ErpNodeDBContext.SystemAccounts.First(a => a.AccountType == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpNodeDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(SystemAccountType key)
        {
            var model = Organization.ErpNodeDBContext.SystemAccounts.First(a => a.AccountType == key);
            Organization.ErpNodeDBContext.SystemAccounts.Remove(model);
            Organization.ErpNodeDBContext.SaveChanges();
        }
    }
}
