using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Assets
{

    public class AssetTypesController : Assets_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.FixedAssetTypes;
            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeper.Node.Models.Assets.FixedAssetType();
            JsonConvert.PopulateObject(values, model);
            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());
            model.Uid = Guid.NewGuid();
            Organization.ErpNodeDBContext.FixedAssetTypes.Add(model);
            Organization.ErpNodeDBContext.SaveChanges();
            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpNodeDBContext.FixedAssetTypes.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpNodeDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            try
            {
                var model = Organization.ErpNodeDBContext.FixedAssetTypes.Find(key);
                Organization.ErpNodeDBContext.FixedAssetTypes.Remove(model);
                Organization.ErpNodeDBContext.SaveChanges();

            }
            catch (Exception) { }

        }
    }
}
