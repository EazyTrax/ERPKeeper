﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.PurchaseQuote
{
    [Route("/API/{CompanyId}/Suppliers/PurchaseQuotes/{Id:Guid}/{controller}/{action=Index}")]

    public class ItemsController : API_Suppliers_PurchaseQuote_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PurchaseQuoteItems
                .Where(r => r.SupplierQuoteId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Suppliers.PurchaseQuoteItem();
            JsonConvert.PopulateObject(values, model);


            var purchaseQuote = Organization.ErpCOREDBContext.PurchaseQuotes.First(a => a.Id == Id);

            if (purchaseQuote.Status != Enterprise.Models.Suppliers.Enums.PurchaseQuoteStatus.Open)
            {
                var item = Organization.ErpCOREDBContext.Items.First(a => a.Id == model.ItemId);

                model.Description = item.Description;
                model.PartNumber = item.PartNumber;
                model.SupplierQuoteId = Id;

                Organization.ErpCOREDBContext.PurchaseQuoteItems.Add(model);
                Organization.ErpCOREDBContext.SaveChanges();
            }

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.PurchaseQuoteItems.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.PurchaseQuoteItems.First(a => a.Id == key);
            Organization.ErpCOREDBContext.PurchaseQuoteItems.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
