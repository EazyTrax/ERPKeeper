using System;
using System.Linq;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERPKeeperCore.Web.Areas.API.Profiles.HR.Employee;

namespace ERPKeeperCore.Web.Areas.API.HR.Employee
{
    [Route("/API/HR/Employees/{EmployeeId:Guid}/{Controller}/{action=Index}")]
    public class WorkRecordsController : _API_HR_Employee_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.WorkRecords
                .Where(a => a.EmployeeId == EmployeeId)
                .OrderByDescending(a => a.RecordDate)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.EmployeeWorkRecord();
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            model.Id = Guid.NewGuid();
            model.EmployeeId = EmployeeId;
            model.RecordDate = DateTime.Now;

            Organization.ErpCOREDBContext.WorkRecords.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.WorkRecords
                .First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.WorkRecords
                .First(a => a.Id == key);
            Organization.ErpCOREDBContext.WorkRecords.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}

