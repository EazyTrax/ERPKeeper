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

    public class AssetsController : API_Assets_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .Assets
                .Include("AssetType");

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Assets.Asset();
            JsonConvert.PopulateObject(values, model);
            model.Status = Node.Models.Assets.Enums.AssetStatus.Draft;

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());


            Organization.ErpCOREDBContext.Assets.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Assets.Find(key);

            if (model.PostStatus == Node.Models.Accounting.Enums.LedgerPostStatus.Editable)
            {
                JsonConvert.PopulateObject(values, model);
                Organization.ErpCOREDBContext.SaveChanges();
                return Ok();
            }
            else
            {
                return Ok("Cannot Edit");
            } 

            
        }

    }
}
