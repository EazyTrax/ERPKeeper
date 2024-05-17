using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Profiles.Profile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.Customer
{
    public class QuotesController : _API_Customers_Customer_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Sales
                .Where(s => s.Status == Enterprise.Models.Customers.Enums.SaleStatus.Draft)
                .Where(r => r.CustomerId == ProfileId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Customers.Sale();
            JsonConvert.PopulateObject(values, model);
            model.Status = Enterprise.Models.Customers.Enums.SaleStatus.Draft;

            model.CustomerId = ProfileId;
            Organization.ErpCOREDBContext.Sales.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Sales.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Sales.First(a => a.Id == key);
            Organization.ErpCOREDBContext.Sales.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
