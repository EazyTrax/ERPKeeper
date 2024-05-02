using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers
{
    public class OrdersController : API_Profiles_Suppliers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Purchases
                 .Where(x => x.Status == Enterprise.Models.Suppliers.Enums.PurchaseStatus.Order)
                 .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Suppliers.Purchase();
            JsonConvert.PopulateObject(values, model);

            model.Status = Enterprise.Models.Suppliers.Enums.PurchaseStatus.Order;

            Organization.ErpCOREDBContext.Purchases.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Purchases.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        //[HttpPost]
        //public void Delete(Guid key)
        //{
        //    var model = Organization.erpNodeDBContext.PurchaseQuotes.First(a => a.Id == key);
        //    Organization.erpNodeDBContext.PurchaseQuotes.Remove(model);
        //    Organization.erpNodeDBContext.SaveChanges();
        //}
    }
}
