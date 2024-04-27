using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{

    public class PaymentPeriodsController : Base_EmployeesController
    {
        public IActionResult Index() =>  View();
        public ActionResult Refresh()
        {
            foreach (var paymentPeriod in EnterpriseRepo.ErpCOREDBContext.EmployeePaymentPeriods.ToList())
                paymentPeriod.Calculate();
            EnterpriseRepo.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
