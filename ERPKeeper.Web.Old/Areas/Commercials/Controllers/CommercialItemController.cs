using ERPKeeper.Node.Models.Transactions;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Commercials.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class CommercialItemController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var item = Organization.CommercialItems.Find(id);
            return PartialView(item);
        }


        public ActionResult Save(CommercialItem commercialItem)
        {
            var item = Organization.CommercialItems.Find(commercialItem.Uid);

            item.SerialNumber = commercialItem.SerialNumber;
            item.Memo = commercialItem.Memo;

            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);




        }
    }
}