using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.API.Accounting.JournalEntry;
using ERPKeeperCore.Web.API.Assets.Asset;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Assets.Asset
{
    public class DeprecateSchedulesController : Asset_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.AssetDeprecateSchedules
                .Where(m => m.AssetId == AssetId);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

       


      
    }
}
