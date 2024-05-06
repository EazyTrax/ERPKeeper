using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.Areas.API.Profiles.Employees.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;



namespace ERPKeeperCore.Web.Areas.API.Employees.Payment
{
    public class ItemsController : _API_Employees_Payment_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.EmployeePaymentItems
                .Where(a => a.EmployeePaymentId == Id)
                .Include(a => a.EmployeePaymentType)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.EmployeePaymentItem();
            JsonConvert.PopulateObject(values, model);

            model.EmployeePaymentId = Id;

            Organization.ErpCOREDBContext.EmployeePaymentItems.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.EmployeePaymentItems
                .First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.EmployeePaymentItems
                .First(a => a.Id == key);


            Organization.ErpCOREDBContext.EmployeePaymentItems
                .Remove(model);

            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
