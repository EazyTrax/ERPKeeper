using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.HR.Employee
{
    [Route("/API/HR/Employees/{EmployeeId:Guid}/{controller}/{action=Index}")]
    public class _API_HR_Employee_BaseController : API_BaseController
    {
        public Guid EmployeeId => Guid.Parse(RouteData.Values["EmployeeId"].ToString());


    }
}
