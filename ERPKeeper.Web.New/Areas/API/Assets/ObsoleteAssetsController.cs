using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Assets
{

    public class ObsoleteAssetsController : API_Assets_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.ObsoleteAssets
                .Include(m=>m.Asset)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        public object Obsolete(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Assets
                .Where(a=>a.ObsoleteAsset!=null)
                .Include(m => m.AssetType)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Assets.Find(key);

            if (!model.IsPosted)
            {
                JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
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
