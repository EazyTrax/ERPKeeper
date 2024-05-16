using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers
{
    public class CustomerQuotesController : API_Profiles_Customers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Customers.SaleQuote();
            JsonConvert.PopulateObject(values, model);

            model.Status = SaleQuoteStatus.Draft;

            Organization.ErpCOREDBContext.SaleQuotes.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.SaleQuotes.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        //[HttpPost]
        //public void Delete(Guid key)
        //{
        //    var model = Organization.erpNodeDBContext.SaleQuotes.First(a => a.Id == key);
        //    Organization.erpNodeDBContext.SaleQuotes.Remove(model);
        //    Organization.erpNodeDBContext.SaveChanges();
        //}
    }
}
