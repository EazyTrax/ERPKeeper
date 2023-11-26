using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.Node.Models.Financial.Payments.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("RetentionType")]
    [Route("{id}/{action=Home}")]

    public class RetentionTypeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.RetentionTypes.Find(id));
        public ActionResult History(Guid id) => View(Organization.RetentionTypes.Find(id));
        public PartialViewResult PartialGridView(Guid id) => PartialView(Organization.RetentionTypes.Find(id));


        public ActionResult New(RetentionDirection id)
        {
            var newRetentionType = Organization.RetentionTypes.CreateNew(id);
            return RedirectToAction("Home", new { id = newRetentionType.Uid });
        }

        public ActionResult Save(RetentionType retentionType)
        {
            var exist = Organization.RetentionTypes.Find(retentionType.Uid);

            if (exist == null)
                Organization.RetentionTypes.Create(retentionType);
            else
                exist.Update(retentionType);

            Organization.SaveChanges();
            return RedirectToAction("Home", new { id = retentionType.Uid });
        }

       
    }

}