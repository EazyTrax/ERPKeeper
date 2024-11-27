using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.Sale
{

    public class ItemsController : _SaleBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleItems
                .Where(r => r.SaleId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {

            _Organization = new Enterprise.EnterpriseRepo(Organization.DatabaseName);

            var model = new Enterprise.Models.Customers.SaleItem();
            JsonConvert.PopulateObject(values, model);

            var sale = Organization.ErpCOREDBContext.Sales.First(a => a.Id == Id);

            if (sale.IsPosted == false)
            {
                var item = Organization.ErpCOREDBContext.Items.First(a => a.Id == model.ItemId);
                model.Description = item.Description;
                model.PartNumber = item.PartNumber;
                model.SaleId = Id;

                Organization.ErpCOREDBContext.SaleItems.Add(model);
                Organization.ErpCOREDBContext.SaveChanges();

            }

            sale.UpdateBalance();
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            _Organization = new Enterprise.EnterpriseRepo(Organization.DatabaseName);

            var model = Organization.ErpCOREDBContext.SaleItems.First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();



            var sale = Organization.ErpCOREDBContext.Sales.First(a => a.Id == Id);
            sale.UpdateBalance();

            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.SaleItems.First(a => a.Id == key);
            Organization.ErpCOREDBContext.SaleItems.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
