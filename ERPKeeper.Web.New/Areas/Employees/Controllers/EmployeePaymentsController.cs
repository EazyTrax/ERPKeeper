using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{

    public class EmployeePaymentsController : _Employees_Base_Controller
    {
        public IActionResult Index() => View();

        public ActionResult Refresh()
        {
            var payments = Organization.ErpCOREDBContext.EmployeePayments
                .Where(x => x.EmployeePaymentPeriodId != null)
                .OrderBy(x => x.EmployeePaymentPeriod.Date)
                .ThenBy(x => x.Employee.Id)
                .ToList();

            int order = 0;

            foreach (var payment in payments)
            {
                payment.No = ++order;
                payment.UpdateBalance();
                payment.UpdateName();
            }


            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
