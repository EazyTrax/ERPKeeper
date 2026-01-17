using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    [Route("/Employee/{Controller=Home}/{Action=Index}")]
    public class _EmployeeBaseController : ERPKeeperCore.Web.Controllers._BaseController
    {
        Guid EmployeeId => AuthorizeUserId;
        public ERPKeeperCore.Enterprise.Models.Employees.Employee Employee
           => Organization.Employees.Find(EmployeeId);

        public _EmployeeBaseController()
        {



        }
    }
}