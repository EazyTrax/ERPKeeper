using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.New.API.Financials.Lend;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.New.API.Financials.Lend
{
    public class LendReturnsController : API_Financials_Lend_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.LendReturns
                .Where(m => m.LendId == LendId);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Financial.LendReturn();
            JsonConvert.PopulateObject(values, model);
            model.LendId = LendId;

            Organization.ErpCOREDBContext.LendReturns.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.LendReturns.Find(key);

            if (model.IsPosted == false)
            {
                JsonConvert.PopulateObject(values, model);
                Organization.ErpCOREDBContext.SaveChanges();

                Organization.ErpCOREDBContext.LendReturns.Find(LendId);

                Organization.SaveChanges();
            }


            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.LendReturns.Find(key);
            if (model.IsPosted == false)
            {
                Organization.ErpCOREDBContext.LendReturns.Remove(model);
                Organization.ErpCOREDBContext.SaveChanges();
            }
        }
    }
}
