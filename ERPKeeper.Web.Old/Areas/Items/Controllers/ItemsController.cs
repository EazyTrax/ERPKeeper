
using ERPKeeper.Node.Models.Items.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{


    [RouteArea("Items")]
    [RoutePrefix("Items")]
    [Route("{action}/{id?}")]

    public class ItemsController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home() => View();
        public ActionResult NonInventory() => View("Home", ItemTypes.NonInventory);
        public ActionResult Inventory() => View("Home", ItemTypes.Inventory);
        public ActionResult Expense() => View("Home", ItemTypes.Expense);
        public ActionResult Service() => View("Home", ItemTypes.Service);
        public ActionResult Asset() => View("Home", ItemTypes.Asset);
        public ActionResult Search() => View();



        public ActionResult New(ItemTypes itemType)
        {
            var item = new Node.Models.Items.Item()
            {
                ItemType = itemType,
                IncomeAccountUid = Organization.ChartOfAccount.IncomeAccounts.FirstOrDefault().Uid,
                PurchaseAccountUid = Organization.ChartOfAccount.ExpenseAccounts.FirstOrDefault().Uid,
            };

            Organization.ErpNodeDBContext.Items.Add(item);
            Organization.SaveChanges();

            return RedirectToAction("Home", "Item", new { id = item.Uid });
        }


        public ActionResult Tile(ItemTypes id = ItemTypes.Service)
        {
            ViewBag.id = id;
            return View();
        }

        public PartialViewResult Partial_ComboBoxItems() => PartialView(Organization.Items.ListAll());
        public ActionResult Export(string DocumentType = "ItemCatalog")
        {
            var Report = new Reports.ItemTag()
            {
                DataSource = Organization.Items.ListOnlineSale(),
                Name = "ItemCatalog"
            };

            ViewBag.Report = Report;

            return View(Report);
        }
        public ActionResult ExportTag()
        {
            var Report = new Reports.ItemTag()
            {
                DataSource = Organization.Items.Query()
                .Where(i => i.ItemType == ItemTypes.Inventory)
                .Where(i => i.OnlineSale)
                .ToList(),
                Name = "ItemTag"
            };

            Report.Parameters["Qty"].Value = "10";
            Report.Parameters["Qty"].Visible = false;

            ViewBag.Report = Report;
            return View(Report);
        }


        public PartialViewResult PartialGridView(ItemTypes id = ItemTypes.Service)
        {
            ViewBag.id = id;
            var itemsList = Organization.Items.ListByType(id);

            return PartialView("PartialGridView", itemsList);
        }


        public PartialViewResult _GridTileItems(ItemTypes id = ItemTypes.Service)
        {
            ViewBag.id = id;
            var items = Organization.Items.ListByType(id);
            return PartialView("_GridTileItems", items);
        }

        public ActionResult UpdateStockAmount()
        {
            Organization.InventoryItemsDal.UpdateStockAmount();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}