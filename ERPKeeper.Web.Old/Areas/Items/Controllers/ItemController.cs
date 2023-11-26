using ERPKeeper.Node.Models.Accounting.Enums;
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
    [RoutePrefix("Item")]
    [Route("{id}/{action=Home}")]

    [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Accountant)]
    public class ItemController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.Items.Find(id));
        public ActionResult Redirect(Guid id)
        {
            var item = Organization.Items.Find(id);
            if (item.ItemType == Node.Models.Items.Enums.ItemTypes.Group)
                return RedirectToAction("Home", "Group", new { id = id });
            else
                return RedirectToAction("Home", "Item", new { id = id });
        }


        public ActionResult Parameters(Guid id) => View(Organization.Items.Find(id));

        public ActionResult Merge(Guid id, Guid mergeToItemId)
        {
            Organization.Items.Merge(id, mergeToItemId);
            return RedirectToAction("Home", new { Id = mergeToItemId });
        }

        public ActionResult Copy(Guid id)
        {
            var item = Organization.Items.Find(id);
            var cloneItem = Organization.Items.Copy(item);
            return RedirectToAction("Home", new { Id = cloneItem.Uid });
        }

        public ActionResult COSG(Guid id)
        {
            ViewBag.ItemId = id;
            return View(Organization.Items.Find(id));
        }

        [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Admin)]
        public ActionResult Delete(Guid id)
        {
            Organization.Items.Delete(id);
            return RedirectToAction("Home", "Items");
        }


        public ActionResult MarkInactive(Guid id)
        {
            var item = Organization.Items.Find(id);
            item.Status = ERPKeeper.Node.Models.Accounting.ItemStatus.InActive;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }




        [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Admin)]
        public ActionResult ChangeType(Guid id, ItemTypes itemType)
        {
            var item = Organization.Items.Find(id);

            if (item != null)
            {
                item.ItemType = itemType;
                Organization.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult History(Guid id) => View("History", Organization.Items.Find(id));


        public PartialViewResult _HistoryTransactionsGridView(Guid id, ERPObjectType? type)
        {
            var model = Organization.CommercialItems.Query().Where(ci => ci.ItemGuid == id);

            if (type != null)
                model = model.Where(ci => ci.Commercial.TransactionType == type);



            ViewBag.Id = id;
            ViewBag.Type = type;

            return PartialView(model.ToList());
        }

        public PartialViewResult PartialGridViewCOSG(Guid id)
        {
            var item = Organization.Items.Find(id);
            var periodItemCOGS = Organization.ItemsCOGS.GetList(item);
            return PartialView(periodItemCOGS);
        }

        public PartialViewResult PartialGridViewKitItems(Guid id)
        {
            var model = Organization.Items.Find(id);
            ViewData["id"] = id;
            return null;
        }
        public ActionResult Images(Guid id) => View(Organization.Items.Find(id));
        public ActionResult DataSheets(Guid id) => View(Organization.Items.Find(id));


        [HttpPost]
        public ActionResult AddImage(Guid id)
        {
            var item = Organization.Items.Find(id);
            var image = System.Web.Helpers.WebImage.GetImageFromRequest();

            if (image != null)
            {
                byte[] toPutInDb = image.GetBytes();
                item.AddImage(toPutInDb);
                Organization.SaveChanges();
            }

            return RedirectToAction("Images", new { id = id });
        }

        [HttpPost]
        [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Accountant)]
        public ActionResult AddDataSheet(Guid id, IEnumerable<HttpPostedFileBase> userDocuments)
        {
            var userDocumentsList = userDocuments as IList<HttpPostedFileBase> ?? userDocuments.ToList();
            var item = Organization.Items.Find(id);

            if (!userDocumentsList.Any())
                ModelState.AddModelError("UserFile", "You must select a file.");

            foreach (var file in userDocumentsList)
            {
                if (file == null || file.FileName == null || file.ContentLength == 0)
                    continue;

                using (var memoryStream = new MemoryStream())
                {
                    file.InputStream.CopyTo(memoryStream);
                    var datasheet = Organization.DataSheets.CreateNew(item, memoryStream, file);

                    Organization.SaveChanges();
                }
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Accountant)]
        public ActionResult RemoveImage(Guid id)
        {
            Organization.ItemImages.Delete(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult NewParameter(Guid id, Guid ItemParameterTypeUid, string value)
        {
            var item = Organization.Items.Find(id);
            var itemParameterType = Organization.ItemParameterTypes.Find(ItemParameterTypeUid);
            item.AddItemParameter(itemParameterType, value);


            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult DataSheet(Guid id)
        {
            var dataSheet = Organization.DataSheets.Find(id);
            return File(dataSheet.File, dataSheet.ContentType, dataSheet.FileName);
        }

        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult GetImage(Guid id)
        {
            var item = Organization.Items.Find(id);

            if (item != null && item.Images.FirstOrDefault() != null)
            {
                WebImage image = new WebImage(item.Images.FirstOrDefault().Image);
                return File(image.GetBytes(), "image/" + image.ImageFormat, image.FileName);
            }
            else
            {
                WebImage defaultimage = new WebImage(Server.MapPath("/Content/Images/default_product.jpg"));
                return File(defaultimage.GetBytes(), "image/" + defaultimage.ImageFormat, defaultimage.FileName);
            }
        }


        [OutputCache(Duration = 3600, VaryByParam = "id")]
        public ActionResult GetImageById(Guid id)
        {
            var itemImage = Organization.ItemImages.Find(id);

            if (itemImage != null)
            {
                WebImage image = new WebImage(itemImage.Image);
                return File(image.GetBytes(), "image/" + image.ImageFormat, image.FileName);
            }
            else
            {
                WebImage defaultimage = new WebImage(Server.MapPath("/Content/Images/default_product.jpg"));
                return File(defaultimage.GetBytes(), "image/" + defaultimage.ImageFormat, defaultimage.FileName);
            }
        }


        [HttpPost]
        [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Accountant)]
        public ActionResult Save(Node.Models.Items.Item model)
        {
            model = Organization.Items.Save(model);
            return RedirectToAction("Home", new { id = model.Uid });
        }

        public ActionResult ExportTag(Guid id, string qty)
        {
            var itemInList = Organization.Items.Query().Where(i => i.Uid == id).ToList();


            var Report = new Reports.ItemTag()
            {
                DataSource = itemInList,
                Name = "ItemTag"
            };

            Report.Parameters["Qty"].Value = qty;
            Report.Parameters["Qty"].Visible = false;

            ViewBag.Report = Report;


            return View(itemInList.FirstOrDefault());
        }

    }
}