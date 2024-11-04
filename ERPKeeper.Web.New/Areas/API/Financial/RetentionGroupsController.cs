using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise.DBContext;
using ERPKeeperCore.Web.New.API.Financials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Financials
{
    public class RetentionGroupsController : API_Financials_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.RetentionPeriods
                .Include(a => a.RetentionType)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Financial.RetentionPeriod();
            JsonConvert.PopulateObject(values, model);

            model.No = Organization.ErpCOREDBContext.RetentionPeriods.Max(x => x.No) + 1;


            Organization.ErpCOREDBContext.RetentionPeriods.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.RetentionPeriods.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);

            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

    }
}
