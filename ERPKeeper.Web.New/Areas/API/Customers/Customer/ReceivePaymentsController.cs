using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.Customer
{
    public class ReceivePaymentsController : _API_Customers_Customer_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.ReceivePayments
                .Where(r => r.Sale.CustomerId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }





        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.ReceivePayments.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }


    }
}
