using ERPKeeper.Node.Models.Employees;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    [RouteArea("Employees")]
    [RoutePrefix("PaymentTemplate")]
    [Route("{id}/{action=Home}")]
    public class PaymentTemplateController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult New()
        {
            return View("Home");
        }
        public ActionResult Home(Guid id)
        {

            var PaymentTemplate = Organization.EmployeePaymentTemplates.Find(id);
            return View(PaymentTemplate);
        }

        public ActionResult Save(EmployeePaymentTemplate template)
        {
            var PaymentTemplate = Organization.EmployeePaymentTemplates.Find(template.Uid);

            if (PaymentTemplate != null)
                PaymentTemplate.Name = template.Name;
            else
                Organization.EmployeePaymentTemplates.CreateNew(template);

            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = template.Uid });
        }


        public PartialViewResult PartialGridView(Guid id)
        {
            var paymentsType = Organization.EmployeePaymentTemplates.GetAll();
            return PartialView(paymentsType);
        }
    }
}