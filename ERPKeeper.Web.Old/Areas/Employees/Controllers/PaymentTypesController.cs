using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    public class PaymentTypesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }



        public ActionResult Home() => View();

        public ActionResult New()
        {
            var paymentsType = Organization.EmployeePaymentTypes.CreateNew();
            return RedirectToAction("Home", new { id = paymentsType.Uid });
        }


        public PartialViewResult PartialGridView()
        {
            var paymentsType = Organization.EmployeePaymentTypes.GetAll();
            return PartialView(paymentsType);
        }



    }
}