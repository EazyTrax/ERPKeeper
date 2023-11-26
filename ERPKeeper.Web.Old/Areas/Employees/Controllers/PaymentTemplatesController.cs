using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    public class PaymentTemplatesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home() => View();

        public PartialViewResult PartialGridView()
        {
            var paymentsType = Organization.EmployeePaymentTemplates.GetAll();
            return PartialView(paymentsType);
        }

    }
}