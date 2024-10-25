using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Employees
{
    public class EmployeePaymentTypesController : API_Employees_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.EmployeePaymentTypes
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.EmployeePaymentType();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpCOREDBContext.EmployeePaymentTypes.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.EmployeePaymentTypes
                .First(a => a.Id == key);

            var value = JsonConvert.DeserializeObject<Enterprise.Models.Employees.EmployeePaymentType>(values);
            model.Name = value.Name;
            model.Description = value.Description;
            model.ExpenseAccountId = value.ExpenseAccountId;

            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }


        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.EmployeePaymentTypes.First(a => a.Id == key);
            Organization.ErpCOREDBContext.EmployeePaymentTypes.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
