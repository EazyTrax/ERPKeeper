﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{
    [Route("/{CompanyId}/Employees/EmployeePaymentTypes/{Id:Guid}/{action=Index}")]
    public class EmployeePaymentTypeController : _Employees_Base_Controller
    {

        public IActionResult Index(Guid Id)
        {
            var Employee = Organization.ErpCOREDBContext
                .EmployeePaymentTypes
                .Find(Id);
            return View(Employee);
        }



    }
}
