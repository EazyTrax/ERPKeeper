using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.Purchase
{
    public class ItemsController : _API_Suppliers_Purchase_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PurchaseItems
                .Where(r => r.PurchaseId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            _Organization = new Enterprise.EnterpriseRepo(Organization.DatabaseName);

            var model = new Enterprise.Models.Suppliers.PurchaseItem();
            JsonConvert.PopulateObject(values, model);

            var Purchase = Organization.ErpCOREDBContext.Purchases.First(a => a.Id == Id);

            if (Purchase.IsPosted == false)
            {
                var item = Organization.ErpCOREDBContext.Items.First(a => a.Id == model.ItemId);
                model.Description = item.Description;
                model.PartNumber = item.PartNumber;
                model.PurchaseId = Id;

                Organization.ErpCOREDBContext.PurchaseItems.Add(model);
                Organization.ErpCOREDBContext.SaveChanges();

            }

            model.PurchaseId = Id;
            Organization.ErpCOREDBContext.PurchaseItems.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            Purchase.UpdateBalance();
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            _Organization = new Enterprise.EnterpriseRepo(Organization.DatabaseName);

            var model = Organization.ErpCOREDBContext.PurchaseItems.First(a => a.Id == key);
            var Purchase = Organization.ErpCOREDBContext.Purchases.First(a => a.Id == Id);

            if (Purchase.IsPosted == false)
            {
                JsonConvert.PopulateObject(values, model);
                Organization.ErpCOREDBContext.SaveChanges();
            }

            Purchase.UpdateBalance();
            Organization.ErpCOREDBContext.SaveChanges();


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
