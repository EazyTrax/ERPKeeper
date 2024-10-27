using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.New.API.Financials.RetentionType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.New.API.Financials.RetentionType
{
    public class RetentionGroupsController : API_Financials_RetentionType_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.RetentionGroups
                .Where(m => m.RetentionTypeId == RetentionTypeId)
                .Include(a => a.RetentionType);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Financial.RetentionGroup();
            JsonConvert.PopulateObject(values, model);
            model.RetentionTypeId = RetentionTypeId;

            Organization.ErpCOREDBContext.RetentionGroups.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.RetentionGroups.Find(key);


            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();



            Organization.SaveChanges();



            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.RetentionGroups.Find(key);


            Organization.ErpCOREDBContext.RetentionGroups.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();

        }
    }
}
