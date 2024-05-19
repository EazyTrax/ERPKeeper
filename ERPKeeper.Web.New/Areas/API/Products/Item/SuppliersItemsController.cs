using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ERPKeeperCore.Web.New.API.Products.Item
{
    public class SuppliersItemsController : API_Products_Item_BaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SupplierItems
                .Where(si => si.ItemId == ItemId)
                .Include(si => si.Supplier)
                .ThenInclude(si => si.Profile);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


    }
}
