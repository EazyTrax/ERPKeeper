﻿using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{

    public class EmployeePaymentsController : _Employees_Base_Controller
    {
        public IActionResult Index() => View();
        public ActionResult Refresh()
        {
            foreach (var payment in OrganizationCore.ErpCOREDBContext.EmployeePayments.ToList())
                payment.UpdateBalance();
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}