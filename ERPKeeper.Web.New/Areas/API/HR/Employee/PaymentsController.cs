using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.Areas.API.Profiles.HR.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ERPKeeperCore.Web.Areas.API.HR.Employee
{
    [Route("/API/HR/Employees/{EmployeeId:Guid}/{Controller}/{action=Index}")]
    public class PaymentsController : _API_HR_Employee_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.EmployeePayments
                .Include(x => x.EmployeePaymentPeriod)
                .Where(a => a.EmployeeId == EmployeeId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.EmployeePaymentPeriodId,
                    EmployeePaymentPeriodName = x.EmployeePaymentPeriod.Name,
                    EmployeePaymentPeriodDate = x.EmployeePaymentPeriod.Date,
                    x.Note,
                    x.TotalEarning,
                    x.TotalDeduction,
                    x.IsPosted
                })
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.EmployeePayment();
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            model.EmployeePaymentPeriodId = EmployeeId;

            Organization.ErpCOREDBContext.EmployeePayments.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.EmployeePayments
                .First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.EmployeePayments
                .First(a => a.Id == key);
            Organization.ErpCOREDBContext.EmployeePayments
                .Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
