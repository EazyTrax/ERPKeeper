


using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Employee.Views
{
    public abstract class EmployeeAreaBasePage<TModel> : ERPKeeperCore.Web.Views.Enterprise_BasePage<TModel>
    {
        public Guid EmployeeId => AuthorizeUserId;

        public ERPKeeperCore.Enterprise.Models.Employees.Employee Employee 
           => Organization.Employees.Find(EmployeeId);


        protected EmployeeAreaBasePage()
        {

        }
    }
}
