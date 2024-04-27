using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{

    public class PaymentsController : Base_EmployeesController
    {
        public IActionResult Index() => View();
        public ActionResult Refresh()
        {
            foreach (var payment in EnterpriseRepo.ErpCOREDBContext.EmployeePayments.ToList())
                payment.Calculate();
            EnterpriseRepo.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
