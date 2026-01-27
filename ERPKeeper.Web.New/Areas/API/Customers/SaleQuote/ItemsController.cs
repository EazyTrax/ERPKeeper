using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.SaleQuote
{

    public class ItemsController : API_Profiles_Customers_SaleQuote_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuoteItems
                .Where(r => r.QuoteId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Customers.SaleQuoteItem();
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            var quote = Organization.ErpCOREDBContext.SaleQuotes.First(a => a.Id == Id);

            if (quote.IsPosted == false)
            {
                var item = Organization.ErpCOREDBContext.Items.First(a => a.Id == model.ItemId);
                model.Description = item.Description;
                model.PartNumber = item.PartNumber;
                model.QuoteId = Id;

                Organization.ErpCOREDBContext.SaleQuoteItems.Add(model);
                Organization.ErpCOREDBContext.SaveChanges();
            }



            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.SaleQuoteItems.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.SaleQuoteItems.First(a => a.Id == key);
            Organization.ErpCOREDBContext.SaleQuoteItems.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
