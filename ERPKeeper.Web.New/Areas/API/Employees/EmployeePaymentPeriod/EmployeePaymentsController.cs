﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.Areas.API.Profiles.Employees.EmployeePaymentPeriod;
using ERPKeeperCore.Web.Areas.API.Profiles.Employees.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ERPKeeperCore.Web.Areas.API.Employees.EmployeePaymentPeriod
{
    public class EmployeePaymentsController : _API_Employees_EmployeePaymentPeriod_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.EmployeePayments
                .Where(a => a.EmployeePaymentPeriodId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.EmployeePayment();
            JsonConvert.PopulateObject(values, model);

            model.EmployeePaymentPeriodId = Id;

            Organization.ErpCOREDBContext.EmployeePayments.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.EmployeePayments
                .First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model);
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
