using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.New.API.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Customers
{
    public class CustomersController : API_Customers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {

            var returnModel = Organization
                .ErpCOREDBContext
                .Customers
                .Include(x => x.Profile)
                .ToList();

 

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Customers.Customer();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpCOREDBContext.Customers.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Customers.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Customers.Find(key);
            Organization.ErpCOREDBContext.Customers.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
