using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Employees.Controllers
{

    public class PaymentsController : Base_EmployeesController
    {
        public IActionResult Index() => View();
        public ActionResult Refresh()
        {
            foreach (var payment in Organization.ErpNodeDBContext.EmployeePayments.ToList())
                payment.Calculate();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
