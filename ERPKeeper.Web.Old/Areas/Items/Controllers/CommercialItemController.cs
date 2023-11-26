using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Items;
using ERPKeeper.Node.Models.Items.Enums;
using ERPKeeper.WebFrontEnd.Authorize;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{

    [RouteArea("Items")]
    [Route("CommercialItem/{id}/{action=Home}")]

    [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Accountant)]
    public class CommercialItemController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult CreateNewPartNumber(Guid id)
        {
            var commercialItem = Organization.CommercialItems.Find(id);


            Item cloneItem;

            cloneItem = Organization.ErpNodeDBContext.Items
                .Where(i => i.PartNumber == commercialItem.ItemPartNumber)
                .FirstOrDefault();


            if (cloneItem == null)
            {
                cloneItem = Organization.Items.Copy(commercialItem.Item);
                cloneItem.PartNumber = commercialItem.ItemPartNumber.Trim();
                cloneItem.Description = commercialItem.ItemDescription;
                cloneItem.UnitPrice = cloneItem.UnitPrice;
                cloneItem.PurchasePrice = cloneItem.UnitPrice;
                Organization.SaveChanges();
            }

            var commercialItems = Organization.ErpNodeDBContext
                .CommercialItems
                .Where(c => c.ItemPartNumber == cloneItem.PartNumber)
                .ToList();

            commercialItems.ForEach(c => c.ItemGuid = cloneItem.Uid);
            Organization.SaveChanges();

            return Content(commercialItems.Count.ToString() + " Item effected");
        }
    }
}