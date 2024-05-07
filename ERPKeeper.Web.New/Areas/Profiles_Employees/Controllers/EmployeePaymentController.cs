using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles_Employees.Controllers
{
    [Route("/{CompanyId}/Profiles/Employees/EmployeePayments/{Id:Guid}/{action=Index}")]
    public class EmployeePaymentController : _Profiles_Employees_Base_Controller
    {

        public IActionResult Index(Guid Id)
        {
            var Employee = OrganizationCore.ErpCOREDBContext
                .EmployeePayments
                .Find(Id);

            return View(Employee);
        }

    }
}
