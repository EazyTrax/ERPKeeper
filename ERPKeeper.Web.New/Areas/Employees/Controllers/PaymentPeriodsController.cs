using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Employees.Controllers
{

    public class PaymentPeriodsController : Base_EmployeesController
    {
        public IActionResult Index() =>  View();
        public ActionResult Refresh()
        {
            foreach (var paymentPeriod in Organization.ErpNodeDBContext.EmployeePaymentPeriods.ToList())
                paymentPeriod.Calculate();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
