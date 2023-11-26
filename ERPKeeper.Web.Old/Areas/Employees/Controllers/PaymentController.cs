using ERPKeeper.Node.Models.Employees;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    [RouteArea("Employees")]
    [RoutePrefix("Payment")]
    [Route("{id}/{action=Home}")]
    public class PaymentController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.EmployeePayments.Find(id));
        public ActionResult Ledger(Guid id) => RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });
        public PartialViewResult PartialGridViewPaymentItem(Guid id) => PartialView(Organization.EmployeePayments.Find(id));


        public PartialViewResult PartialGridViewPaymentItemUpdate(EmployeePaymentItem paymentLine)
        {
            var paymentItem = Organization.EmployeePaymentLines.Find(paymentLine.Uid);
            var payment = paymentItem.EmployeePayment;

            if (payment.PostStatus == Node.Models.Accounting.Enums.LedgerPostStatus.Editable)
            {
                if (paymentLine.Amount != 0)
                    paymentItem.UpdateAmount(paymentLine);
                else
                    Organization.EmployeePaymentLines.Remove(paymentItem);
            }

            payment.Calculate();
            Organization.SaveChanges();
            return PartialView("PartialGridViewPaymentItem", payment);
        }

        public PartialViewResult _ComboBoxStub()
        {
            return PartialView();
        }

        public ActionResult AddPayItem(Guid id, Guid PaymentTypeGuid, decimal Amount)
        {
            var payment = Organization.EmployeePayments.Find(id);
            var paymentType = Organization.EmployeePaymentTypes.Find(PaymentTypeGuid);
            var paymentItem = payment.addPaymentItems(paymentType, Amount);
            payment.Calculate();
            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = id });
        }

        public ActionResult Save(ERPKeeper.Node.Models.Employees.EmployeePayment payment)
        {
            Organization.EmployeePayments.Save(payment);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Delete(Guid id)
        {
            Organization.EmployeePayments.Delete(id);
            return RedirectToAction("Home", "Payments");
        }

        public ActionResult Copy(Guid id)
        {
            var EmployeePayment = Organization.EmployeePayments.Find(id);
            var cloneEmployeePayment = Organization.EmployeePayments.Copy(EmployeePayment, DateTime.Now);
            return RedirectToAction("Home", "Payment", new { id = cloneEmployeePayment.Uid });
        }

        public ActionResult UnPost(Guid id)
        {
            var payment = Organization.EmployeePayments.Find(id);
            Organization.EmployeePayments.UnPost(payment);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Export(Guid id)
        {
            var payments = Organization.EmployeePayments.GetQuery().Where(ep => ep.Uid == id).ToList();

            var Report = new Node.Reports.Employees.Payment()
            {
                DataSource = payments,
                Name = id.ToString("D")
            };

            ViewBag.Report = Report;

            return View(payments.FirstOrDefault());
        }


    }
}