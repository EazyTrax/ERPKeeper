
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{
    public class COSGController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(Guid? id)
        {
            ViewBag.Id = id;
            return View();
        }

        public PartialViewResult PartialGridView(Guid? id)
        {
            ViewBag.Id = id;

            if (id == null)
            {
                var periodItemCOGS = Organization.ItemsCOGS.GetList();
                return PartialView("PartialGridView", periodItemCOGS);
            }
            else
            {
                var fiscal = Organization.FiscalYears.Find((Guid)id);
                var periodItemCOGS = Organization.ItemsCOGS.GetList(fiscal);

                return PartialView("PartialGridView", periodItemCOGS);
            }
        }


        public ActionResult Update()
        {
            Organization.Items.RemoveUnLinkCommercialAndEstimateItems();

            Organization.ItemsCOGS.CreateCOSG();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}