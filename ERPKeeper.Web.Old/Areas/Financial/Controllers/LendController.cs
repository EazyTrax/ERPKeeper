using ERPKeeper.Node.Models.Financial.Lends;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("Lend")]
    [Route("{id}/{action=Home}")]
    public class LendController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.Lends.Find(id));

        public ActionResult Ledger(Guid id) => RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });


        public ActionResult New()
        {
            var transaction = Organization.Lends.Find(Guid.Empty);
            var lend = new Lend()
            {
                TrnDate = DateTime.Now,
                Uid = Guid.NewGuid(),
            };

            return View("Home", lend);
        }

        [HttpPost]
        public ActionResult Save(Lend lend)
        {
            Organization.Lends.Save(lend);

            return RedirectToAction("Home", new { id = lend.Uid });
        }

        public ActionResult Issue(Guid id)
        {
            var lend = Organization.Lends.Find(id);
            Organization.Lends.Issue(lend);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnPost(Guid id)
        {
            var lend = Organization.Lends.Find(id);
            Organization.Lends.UnPost(lend);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Delete(Guid id)
        {
            Organization.Lends.Delete(id);

            return RedirectToAction("Home", "Lends");
        }




    }
}