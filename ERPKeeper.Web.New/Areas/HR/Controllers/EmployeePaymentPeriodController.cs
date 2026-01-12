using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ERPKeeperCore.Web.Areas.HR.Controllers
{
    [Route("/HR/Employees/EmployeePaymentPeriods/{Id:Guid}/{action=Index}")]
    public class EmployeePaymentPeriodController : _Employees_Base_Controller
    {

        public IActionResult Index(Guid Id)
        {
            var employeePaymentPeriod = Organization.ErpCOREDBContext
                .EmployeePaymentPeriods
                .Find(Id);

            return View(employeePaymentPeriod);
        }
        public ActionResult Refresh(Guid Id)
        {
            var employeePaymentPeriod = Organization.ErpCOREDBContext
              .EmployeePaymentPeriods
              .Find(Id);

            employeePaymentPeriod.UpdateBalance();

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult Delete(Guid Id)
        {
            var employeePaymentPeriod = Organization.ErpCOREDBContext
              .EmployeePaymentPeriods
              .Find(Id);

            if (employeePaymentPeriod.EmployeePayments.Count == 0)
                Organization.ErpCOREDBContext
                    .EmployeePaymentPeriods
                    .Remove(employeePaymentPeriod);

            Organization.SaveChanges();

            return Redirect($"/Employees/EmployeePaymentPeriods");
        }
    }
}
