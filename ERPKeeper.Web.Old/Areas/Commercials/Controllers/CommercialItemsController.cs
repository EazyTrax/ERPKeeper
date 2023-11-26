using ERPKeeper.Node.Models.Accounting.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Commercials.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class CommercialItemsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public PartialViewResult Partial_ComboBoxItems(ERPObjectType transactionType)
        {
            ViewBag.transactionType = transactionType;

            var items = Organization.Items.GetItems(transactionType);

            return PartialView(items);
        }

    }
}