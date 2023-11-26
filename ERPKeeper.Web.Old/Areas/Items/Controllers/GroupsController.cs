using ERPKeeper.Node.Models.Items;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{
    public class GroupsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult _TreeList() => PartialView(Organization.GroupItems.GetListAll());



        public ActionResult New(String GroupName, Guid? ParentUid)
        {
            if (GroupName != null)
            {
                var newGroup = Organization.GroupItems.FindOrCreate(GroupName, ParentUid);
                return RedirectToAction("Home", "Group", new { id = newGroup.Uid });
            }
            else
                return RedirectToAction("Home", "Groups");
        }


        public ActionResult Save(ItemGroup group)
        {
            var existGroup = Organization.GroupItems.Find(group.Uid);
            existGroup.UpdateGroupData(group);
            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = group.Uid });
        }
      
        public ActionResult DragAndDrop()
        {
            Guid Uid = Guid.Parse(Request.Params["Uid"]);
            var child = Organization.GroupItems.Find(Uid);

            Guid parentUid;
            try
            {
                parentUid = Guid.Parse(Request.Params["ParentUid"]);
                var targetParent = Organization.GroupItems.Find(parentUid);

                if (child != null)
                {
                    if (!child.IsChild(targetParent))
                    {
                        child.SetParent(targetParent);
                    }
                }
            }
            catch (Exception)
            {
                // childNode.ParentUid = ParentUid;
            }
            Organization.SaveChanges();


            return PartialView("_TreeList", Organization.GroupItems.GetListAll());
        }


    }
}