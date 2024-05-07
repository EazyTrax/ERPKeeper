using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Assets
{

    public class AssetDeprecateSchedulesController : API_Assets_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.AssetDeprecateSchedules;
            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Assets.AssetDeprecateSchedule();
            JsonConvert.PopulateObject(values, model);
            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());
            
            Organization.ErpCOREDBContext.AssetDeprecateSchedules.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.AssetDeprecateSchedules.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            try
            {
                var model = Organization.ErpCOREDBContext.AssetDeprecateSchedules.Find(key);
                Organization.ErpCOREDBContext.AssetDeprecateSchedules.Remove(model);
                Organization.ErpCOREDBContext.SaveChanges();

            }
            catch (Exception) { }

        }
    }
}
