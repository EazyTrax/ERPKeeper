﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{
    [Route("/{CompanyId}/Employees/EmployeePayments/{Id:Guid}/{action=Index}")]
    public class EmployeePaymentController : _Employees_Base_Controller
    {

        public IActionResult Index(Guid Id)
        {
            var Employee = Organization.ErpCOREDBContext
                .EmployeePayments
                .Find(Id);

            return View(Employee);
        }

    }
}
