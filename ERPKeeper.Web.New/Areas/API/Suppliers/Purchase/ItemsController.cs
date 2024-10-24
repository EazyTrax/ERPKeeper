﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.Purchase
{
    public class ItemsController : _PurchaseBaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PurchaseItems
                .Where(r => r.PurchaseId == PurchaseId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Suppliers.PurchaseItem();
            JsonConvert.PopulateObject(values, model);

            var Purchase = Organization.ErpCOREDBContext.Purchases.First(a => a.Id == PurchaseId);

            if (Purchase.IsPosted == false)
            {
                var item = Organization.ErpCOREDBContext.Items.First(a => a.Id == model.ItemId);
                model.Description = item.Description;
                model.PartNumber = item.PartNumber;
                model.PurchaseId = PurchaseId;

                Organization.ErpCOREDBContext.PurchaseItems.Add(model);
                Organization.ErpCOREDBContext.SaveChanges();

            }

            model.PurchaseId = PurchaseId;
            Organization.ErpCOREDBContext.PurchaseItems.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.PurchaseItems.First(a => a.Id == key);
            var Purchase = Organization.ErpCOREDBContext.Purchases.First(a => a.Id == PurchaseId);

            if (Purchase.IsPosted == false)
            {
                JsonConvert.PopulateObject(values, model);
                Organization.ErpCOREDBContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.PurchaseItems.First(a => a.Id == key);
            Organization.ErpCOREDBContext.PurchaseItems.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
