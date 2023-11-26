using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeper.Web.New.API.Accounting;
using ERPKeeper.Web.New.API.Accounting.JournalEntry;
using ERPKeeper.Web.New.API.Assets.Asset;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Assets.Asset
{
    public class DeprecateSchedulesController : Asset_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.DeprecateSchedules
                .Where(m => m.FixedAssetUid == AssetId);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

       


      
    }
}
