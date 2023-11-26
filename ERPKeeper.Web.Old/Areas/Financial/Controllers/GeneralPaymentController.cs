using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.WebFrontEnd.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("GeneralPayment")]
    [Route("{id}/{action=Home}")]
    public class GeneralPaymentController : _DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var payment = Organization.GeneralPayments.Find(id);

            if (payment == null)
                return Content("ERROR, Payment not found");
            else if (payment.TransactionType == ERPObjectType.ReceivePayment)
                return RedirectToAction("Home", "ReceivePayment", new { Area = "Customers", Id = id });
            else if (payment.TransactionType == ERPObjectType.SupplierPayment)
                return RedirectToAction("Home", "SupplierPayment", new { Area = "Suppliers", Id = id });
            else if (payment.TransactionType == ERPObjectType.LiabilityPayment)
                return RedirectToAction("Home", "LiabilityPayment", new { Area = "Financial", Id = id });
            else
                return Content("ERROR, Payment not found");
        }
        public PartialViewResult _DivRetentions(Guid id) => PartialView(Organization.GeneralPayments.Find(id));
        public PartialViewResult PartialGridViewPaymentRetentions(Guid id) => PartialView(Organization.GeneralPayments.Find(id));
        public PartialViewResult _DivPayFrom(Guid id) => PartialView(Organization.GeneralPayments.Find(id));
        public PartialViewResult PartialGridViewPayFrom(Guid id) => PartialView(Organization.GeneralPayments.Find(id));

        //public ActionResult Download(Guid id)
        //{
        //    var exist = Organization.RetentionTypes.Find(id);

        //    int i = 1;
        //    string retention = string.Empty;
        //    Organization.ErpNodeDBContext
        //        .PaymentRetentions
        //        .Where(s => s.RetentionTypeId == id)
        //        .OrderBy(s => s.GeneralPayment.TrnDate)
        //        .ToList()
        //        .ForEach(s => retention = retention + s.GetThaiRDReport(i++) + Environment.NewLine);

        //    return File(System.Text.Encoding.UTF8.GetBytes(retention),
        //         "text/plain",
        //          $"{exist.Name}_{DateTime.Now.ToShortDateString()}.txt");
        //}
    }
}