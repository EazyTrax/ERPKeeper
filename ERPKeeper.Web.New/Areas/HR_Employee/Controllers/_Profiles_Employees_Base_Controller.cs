using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.HR_Employee.Controllers
{
    [Area("HR_Employee")]
    [Route("/HR/Employees/{EmployeeId:Guid}/{controller=Home}/{action=Index}")]
    public class _Employee_Base_Controller : _BaseController
    {
        public Guid EmployeeId => Guid.Parse(HttpContext.Request.RouteValues["EmployeeId"].ToString());

        public Employee Employee => Organization.Employees.Find(EmployeeId);
    }
}