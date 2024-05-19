using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.Customer
{
    public class ItemsController : _API_Customers_Customer_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.CustomerItems
                .Where(r => r.CustomerId == ProfileId)
                .Include(m=>m.Item)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

    }
}
