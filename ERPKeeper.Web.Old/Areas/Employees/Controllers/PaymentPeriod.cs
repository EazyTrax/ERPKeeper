using ERPKeeper.Node.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    [RouteArea("Employees")]
    [RoutePrefix("PaymentPeriod")]
    [Route("{id}/{action=Home}")]
    public class PaymentPeriodController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var stub = Organization.EmployeePaymentPeriods.Find(id);
            return View(stub);
        }

        public PartialViewResult PartialGridView(Guid id)
        {
            var paymentPeriod = Organization.EmployeePaymentPeriods.Find(id);
            return PartialView(paymentPeriod.EmployeePayments.ToList());
        }


        public ActionResult Export(Guid id)
        {
            var paymentPeriod = Organization.EmployeePaymentPeriods.Find(id);
            List<EmployeePaymentPeriod> paymentPeriods = new List<EmployeePaymentPeriod>() { paymentPeriod };


            var ReportPaymentGroup = new Node.Reports.Employees.PaymentGroup()
            {
                DataSource = paymentPeriods,
                Name = id.ToString("D")
            };
            ReportPaymentGroup.CreateDocument();

            var ReportPayments = new Node.Reports.Employees.Payment()
            {
                DataSource = paymentPeriod.EmployeePayments.ToList(),
                Name = id.ToString("D")
            };
            ReportPayments.CreateDocument();


            ReportPaymentGroup.Pages.AddRange(ReportPayments.Pages);
            ReportPaymentGroup.PrintingSystem.ContinuousPageNumbering = true;

            ViewBag.Report = ReportPaymentGroup;
            return View(paymentPeriod);
        }





        public ActionResult Save(EmployeePaymentPeriod period)
        {
            var exPeriod = Organization.EmployeePaymentPeriods.Find(period.Uid);
            exPeriod.Update(period);
            exPeriod.Calculate();

            Organization.SaveChanges();
            return RedirectToAction("Home", new { id = exPeriod.Uid });
        }



    }
}