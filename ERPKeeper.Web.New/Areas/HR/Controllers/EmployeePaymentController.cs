using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.HR.Controllers
{
    [Route("/HR/EmployeePayments/{Id:Guid}/{action=Index}")]
    public class EmployeePaymentController : _Employees_Base_Controller
    {

        public IActionResult Index(Guid Id)
        {
            var Employee = Organization.ErpCOREDBContext
                .EmployeePayments
                .Find(Id);

            return View(Employee);
        }
        public IActionResult Post(Guid Id)
        {
            var model = Organization.EmployeePayments.Find(Id);
            Organization.EmployeePayments.Post(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult UnPost(Guid Id)
        {
            var model = Organization.EmployeePayments.Find(Id);
            Organization.EmployeePayments.UnPostLedgers(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Delete(Guid Id)
        {
            var model = Organization.EmployeePayments.Find(Id);
            Organization.EmployeePayments.Remove(model);
            Organization.SaveChanges();

            return Redirect($"/HR/EmployeePayments");
        }
    }
}
