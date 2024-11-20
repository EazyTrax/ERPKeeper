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
    [Route("/API/{CompanyId}/Customers/SaleQuotes/{Id:Guid}/{controller}/{action=Index}")]

    public class ItemsController : API_Profiles_Customers_SaleQuote_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuoteItems
                .Where(r => r.QuoteId == SaleQuoteId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Customers.SaleQuoteItem();
            JsonConvert.PopulateObject(values, model);

            var quote = Organization.ErpCOREDBContext.SaleQuotes.First(a => a.Id == SaleQuoteId);

            if (quote.IsPosted == false)
            {
                var item = Organization.ErpCOREDBContext.Items.First(a => a.Id == model.ItemId);
                model.Description = item.Description;
                model.PartNumber = item.PartNumber;
                model.QuoteId = SaleQuoteId;

                Organization.ErpCOREDBContext.SaleQuoteItems.Add(model);
                Organization.ErpCOREDBContext.SaveChanges();
            }



            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.SaleQuoteItems.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
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
