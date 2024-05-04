using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers
{
    public class ReceivePaymentsController : API_Profiles_Customers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.ReceivePayments
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Customers.ReceivePayment();
            JsonConvert.PopulateObject(values, model);

     
            Organization.ErpCOREDBContext.ReceivePayments.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.ReceivePayments.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        //[HttpPost]
        //public void Delete(Guid key)
        //{
        //    var model = Organization.erpNodeDBContext.ReceivePayments.First(a => a.Id == key);
        //    Organization.erpNodeDBContext.ReceivePayments.Remove(model);
        //    Organization.erpNodeDBContext.SaveChanges();
        //}
    }
}
