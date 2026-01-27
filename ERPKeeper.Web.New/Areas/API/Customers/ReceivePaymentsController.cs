using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers
{
    public class ReceivePaymentsController : API_Profiles_Customers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .ReceivePayments
                .Include(a => a.Sale);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Customers.ReceivePayment();

          

            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);


            Organization.ErpCOREDBContext.ReceivePayments.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.ReceivePayments.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
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
