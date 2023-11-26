using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Taxes.Controllers
{
    public class IncomeTaxesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() => PartialView(Organization.IncomeTaxes.GetList());

        public ActionResult Create()
        {
            var incomeTax = Organization.IncomeTaxes.Create();

            return RedirectToAction("Home", "IncomeTax", new { Id = incomeTax.Uid });
        }



        public ActionResult ReOrder()
        {
            Organization.IncomeTaxes.ReOrder();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}