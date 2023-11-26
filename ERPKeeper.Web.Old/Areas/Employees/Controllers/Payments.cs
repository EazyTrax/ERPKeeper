using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    public class PaymentsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home()
        {

            return View();
        }

        public PartialViewResult PartialGridView()
        {

            return PartialView(Organization.EmployeePayments.GetQuery().ToList());
        }

        public ActionResult ReOrderNo()
        {
            Organization.EmployeePayments.Reorders();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


    



    }
}