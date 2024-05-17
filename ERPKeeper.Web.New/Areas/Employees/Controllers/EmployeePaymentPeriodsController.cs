using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{

    public class EmployeePaymentPeriodsController : _Employees_Base_Controller
    {
        public IActionResult Index() =>  View();
        public ActionResult Refresh()
        {
            foreach (var paymentPeriod in OrganizationCore.ErpCOREDBContext.EmployeePaymentPeriods.ToList())
                paymentPeriod.UpdateBalance();

            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
