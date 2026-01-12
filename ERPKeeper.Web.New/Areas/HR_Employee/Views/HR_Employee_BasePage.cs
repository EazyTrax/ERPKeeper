using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Web.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.HR_Employee.Views
{
    public abstract class HR_Employee_BasePage<TModel> : Enterprise_BasePage<TModel>
    {
        public Guid EmployeeId => Guid.Parse(ViewContext.RouteData.Values["EmployeeId"]?.ToString() ?? string.Empty);

        public Employee Employee => Organization.Employees.Find(EmployeeId);

    }
}       
