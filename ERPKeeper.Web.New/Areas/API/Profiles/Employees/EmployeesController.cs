using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Employees
{
    public class EmployeesController : API_Profiles_Employees_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {

            var returnModel = Organization
                .ErpCOREDBContext
                .Employees
                .Include(x => x.Profile)
                .ToList();



            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.Employee();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpCOREDBContext.Employees.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Employees.First(a => a.ProfileId == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Employees.First(a => a.ProfileId == key);
            Organization.ErpCOREDBContext.Employees.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
