using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Accountant,Administrator")]
    [Route("/Employee/{Controller=Home}/{Action=Index}")]
    public class EmployeeBaseController : _EmployeeRole_BaseController
    {
        Guid EmployeeId => AuthorizeUserId;
        public Enterprise.Models.Employees.Employee Employee
           => Organization.Employees.Find(EmployeeId);

        public EmployeeBaseController()
        {



        }
    }
}