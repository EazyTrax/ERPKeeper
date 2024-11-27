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

    public class ShipmentsController : _SaleBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Shipments
                .Where(r => r.TransactionId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Logistic.Shipment();
            JsonConvert.PopulateObject(values, model);

            model.TransactionId = Id;

            Organization.ErpCOREDBContext.Shipments.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Shipments.First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Shipments.First(a => a.Id == key);
            Organization.ErpCOREDBContext.Shipments.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
