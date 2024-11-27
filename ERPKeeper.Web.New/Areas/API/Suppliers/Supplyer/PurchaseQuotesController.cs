using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.Supplyer
{
    public class PurchaseQuotesController : _API_Suppliers_Supplier_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PurchaseQuotes
                .Where(r => r.SupplierId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Suppliers.PurchaseQuote();
            JsonConvert.PopulateObject(values, model);



            model.Status = Enterprise.Models.Suppliers.Enums.PurchaseQuoteStatus.Open;
            model.SupplierId = Id;

            Organization.PurchaseQuotes.New(model);
            Organization.SaveChanges();


            return Ok();
        }



        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.PurchaseQuotes.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.PurchaseQuotes.First(a => a.Id == key);
            Organization.ErpCOREDBContext.PurchaseQuotes.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
