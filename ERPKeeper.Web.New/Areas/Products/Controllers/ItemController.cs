
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Products.Controllers
{
    [Route("/{CompanyId}/{area}/Items/{ItemUid:Guid}/{action=Index}")]
    public class ItemController : Base_ProductsController
    {
        public Guid ItemUid => Guid.Parse(RouteData.Values["ItemUid"].ToString());
        public Node.Models.Items.Item Item => Organization.Items.Find(ItemUid);

        public IActionResult Index() => View(Item);
        public IActionResult Estimates() => View(Item);
        public IActionResult Purchases() => View(Item);
        public IActionResult Sales() => View(Item);

        [HttpPost]
        public IActionResult Update(ERPKeeper.Node.Models.Items.Item model)
        {
            Item.UpdateFromNewERP(model);
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult Copy()
        {
            var item = Organization.Items.Find(ItemUid);
            var cloneItem = Organization.Items.Copy(item);
            return Redirect($"/{CompanyId}/Products/Items/{cloneItem.Uid}");
        }
        public ActionResult Delete()
        {
            var item = Organization.Items.Find(ItemUid);
            Organization.Items.Delete(item.Uid);

            return Redirect($"/{CompanyId}/Products/Items/");
        }

        public IActionResult UpdateStock()
        {
            Organization.InventoryItemsDal.UpdateStockAmount();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
