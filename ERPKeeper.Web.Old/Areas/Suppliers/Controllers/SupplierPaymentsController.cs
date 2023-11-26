

using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{

    [RouteArea("Suppliers")]
    [RoutePrefix("SupplierPayments")]
    [Route("{action}")]
    public class SupplierPaymentsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home() => View();

        public PartialViewResult PartialGridView() => PartialView("PartialGridView", Organization.SupplierPayments.GetAll());

        public ActionResult ReOrder()
        {
            Organization.SupplierPayments.Reorder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Check()
        {
            Organization.SupplierPayments.RunCheck();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}