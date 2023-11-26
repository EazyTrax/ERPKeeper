using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.WebFrontEnd.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("LiabilityPayment")]
    [Route("{id}/{action=Home}")]

    public class LiabilityPaymentController : _DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.LiabilityPayments.Find(id));
        public ActionResult UpdateBalance(Guid id)
        {
            var existLiabilityPayment = Organization.LiabilityPayments.Find(id);
            existLiabilityPayment.CalculateAmount();
            Organization.LiabilityPayments.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Save(LiabilityPayment LiabilityPayment)
        {
            var existLiabilityPayment = Organization.LiabilityPayments.Find(LiabilityPayment.Uid);

            existLiabilityPayment.Update(LiabilityPayment);

            if (existLiabilityPayment.AssetAccountUid == null)
                existLiabilityPayment.AssetAccountUid = Organization.SystemAccounts.Cash.Uid;

            Organization.LiabilityPayments.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Delete(Guid id)
        {
            var payment = Organization.LiabilityPayments.Find(id);
            Organization.LiabilityPayments.Remove(payment);

            return RedirectToAction("Home", "LiabilityPayments");
        }
        public ActionResult Copy(Guid id)
        {
            var LiabilityPayment = Organization.LiabilityPayments.Find(id);
            var cloneLiabilityPayment = Organization.LiabilityPayments.Copy(LiabilityPayment, DateTime.Now);
            return RedirectToAction("Home", "LiabilityPayment", new { id = cloneLiabilityPayment.Uid });
        }
        public ActionResult UnPost(Guid id)
        {
            var payment = Organization.LiabilityPayments.Find(id);
            Organization.LiabilityPayments.UnPost(payment);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult ReOrder(LiabilityPayment LiabilityPayment)
        {
            Organization.LiabilityPayments.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Ledger(Guid id) => RedirectToAction("Home", "Ledgers",
            new
            {
                Area = "Accounting",
                id = id
            });
    }
}