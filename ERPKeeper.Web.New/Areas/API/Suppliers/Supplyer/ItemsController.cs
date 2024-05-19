using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.Supplyer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.Supplier
{
    public class ItemsController : _API_Suppliers_Supplier_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SupplierItems
                .Where(r => r.SupplierId == ProfileId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

    }
}
