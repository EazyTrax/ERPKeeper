using ERPKeeper.Node.Models.Items;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{
    public class GroupController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var itemGroup = Organization.GroupItems.Find(id);
            return View(itemGroup);
        }

        public ActionResult Parameters(Guid id)
        {
            var itemGroup = Organization.GroupItems.Find(id);
            return View(itemGroup);
        }

        public ActionResult Items(Guid id)
        {
            var itemGroup = Organization.GroupItems.Find(id);
            return View(itemGroup);
        }

        public ActionResult New(String GroupName, Guid? ParentGroupGuid)
        {
            var newItemGroup = Organization.GroupItems.FindOrCreate(GroupName, ParentGroupGuid);
            return RedirectToAction("Home", new { id = newItemGroup.Uid });
        }

        public ActionResult Save(GroupItem GroupItem)
        {
            var existItemGroup = Organization.GroupItems.Find(GroupItem.Uid);

            existItemGroup.Name = GroupItem.Name;
            existItemGroup.Detail = GroupItem.Detail;
            existItemGroup.ParentUid = GroupItem.ParentUid;

            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult ApplyPriceRange(Guid id)
        {
            var itemGroup = Organization.GroupItems.Find(id);

            //if (itemGroup.DefaultPriceRange != null)
            //{
            //    itemGroup..ToList().ForEach(i =>
            //    {
            //        i.PriceRangeUid = itemGroup.DefaultPriceRangeUid;
            //    });
            //}

            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Delete(Guid id)
        {
            Organization.GroupItems.Delete(id);

            return RedirectToAction("Home", "Groups");
        }

        public ActionResult AddItem(Guid id, Guid childId)
        {
            var groupItem = Organization.GroupItems.Find(id);
            var childGroup = Organization.GroupItems.Find(childId);

            groupItem.Child.Add(childGroup);

            Organization.SaveChanges();
            return RedirectToAction("Home", new { id = id });
        }

        public ActionResult NewParameter(Guid id, Guid ItemParameterTypeUid, string value)
        {
            var item = Organization.Items.Find(id);
            var itemParameterType = Organization.ItemParameterTypes.Find(ItemParameterTypeUid);
            item.AddItemParameter(itemParameterType, value);


            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public PartialViewResult PartialGridView(Guid id)
        {
            ViewBag.Id = id;
            var groupItem = Organization.GroupItems.Find(id);
            return PartialView("PartialGridView", groupItem);
        }
    }
}