using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    [RouteArea("Accounting")]
    [RoutePrefix("FiscalYears")]
    [Route("{action}/{id?}")]
    public class FiscalYearsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();

        [ValidateInput(false)]
        public PartialViewResult PartialGridView()
        {
            var model = Organization.FiscalYears.GetHistoryList();
            return PartialView("PartialGridView", model);
        }

        public ActionResult AutoCreate()
        {
            var firstDate = Organization.FirstDate;
            while (firstDate < DateTime.Now.AddYears(5))
            {
                Organization.FiscalYears.Find(firstDate);
                firstDate = firstDate.AddYears(1);
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}