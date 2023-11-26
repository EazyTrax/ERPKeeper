using ERPKeeper.Node.Models.Employees;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    [RouteArea("Employees")]
    [RoutePrefix("PaymentType")]
    [Route("{id}/{action=Home}")]
    public class PaymentTypeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var stub = Organization.EmployeePaymentTypes.Find(id);
            return View(stub);
        }

        public ActionResult Payments(Guid id)
        {
            var stub = Organization.EmployeePaymentTypes.Find(id);
            Organization.EmployeePaymentTypes.ClearEmpty();
            return View(stub);

        }
        public PartialViewResult PartialGridViewPayments(Guid id)
        {
            var stub = Organization.EmployeePaymentTypes.Find(id);


            return PartialView(stub);
        }


     
        public ActionResult Save(EmployeePaymentType paymentType)
        {
            var stub = Organization.EmployeePaymentTypes.Find(paymentType.Uid);

            if (stub != null)
                stub.Update(paymentType);
            else
                stub = Organization.EmployeePaymentTypes.CreateNew(paymentType);

            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = stub.Uid });
        }


    }
}