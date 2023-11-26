using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Security.Enums;
using ERPKeeper.Node.Models.Transactions;
using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{
    [RouteArea("Suppliers")]
    [RoutePrefix("Purchase")]
    [Route("{id}/{action=Home}")]
    public class PurchaseController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(Guid id)
        {
            var transaction = Organization.Commercials.Find(id);
            return View(transaction);
        }

        public ActionResult Items(Guid id)
        {
            var transaction = Organization.Commercials.Find(id);
            Organization.AssistItems.Create(transaction.ProfileGuid);
            return View(transaction);
        }

        public ActionResult Payments(Guid id)
        {
            var transaction = Organization.Purchases.Find(id);
            ViewBag.id = id;
            return View(transaction);
        }


        public ActionResult CreatePayment(Guid id, DateTime? payDate = null)
        {
            var purchase = Organization.Purchases.Find(id);

            if (purchase.GeneralPaymentUid != null)
                return Content("Commercial is paid");

            var payment = Organization.SupplierPayments.Create(purchase.Profile, purchase, payDate);
            return RedirectToAction("Home", "SupplierPayment", new { id = payment.Uid });
        }


        public ActionResult Documents(Guid id)
        {
            var transaction = Organization.Commercials.Find(id);
            return View(transaction);
        }

        [HttpPost]
        public ActionResult Save(Purchase purchase)
        {
            purchase = Organization.Purchases.Update(purchase);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }





        public ActionResult Export(Guid id, string DocumentType)
        {
            var transactions = Organization.Purchases.GetQueryable()
                .Where(tr => tr.Uid == id)
                .Take(1)
                .ToList();

            var Report = new Node.Reports.Suppliers.Purchase()
            {
                DataSource = transactions,
                Name = id.ToString("D")
            };
            Report.Parameters["DocumentType"].Value = DocumentType;
            Report.Parameters["DocumentType"].Visible = false;



            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;
            Report.Parameters["CompanyName"].Visible = false;

            ViewBag.Report = Report;

            return View("Export", transactions.FirstOrDefault());
        }


        public ActionResult Ledger(Guid id)
        {
            return RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });
        }

        public ActionResult JournalItems(Guid id)
        {
            var transaction = Organization.Commercials.Find(id);
            return View(transaction);
        }

        public PartialViewResult _PaymentSummary(Guid id)
        {
            var purchase = Organization.Purchases.Find(id);
            return PartialView(purchase);
        }



        public ActionResult UnPost(Guid id)
        {
            var purchase = Organization.Purchases.Find(id);
            Organization.Purchases.UnPost(purchase);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult PostLedger(Guid id)
        {
            var purchase = Organization.Purchases.Find(id);
            Organization.Purchases.PostLedger(purchase);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        [Authorize.ERPAuthorize(AccessLevel.Accountant)]
        public ActionResult Delete(Guid id)
        {
            var purchase = Organization.Purchases.Find(id);

            if (purchase == null)
                return Content("Cannot Find purchase. ");
            else if (purchase.PostStatus == LedgerPostStatus.Locked)
                return Content("Ledger Posted!. ");
            else
                Organization.Purchases.Delete(purchase);

            return RedirectToAction("Home", "Purchases", new { });
        }

        [Authorize.ERPAuthorize(AccessLevel.TransactionMaker)]
        public ActionResult Copy(Guid id)
        {
            var purchase = Organization.Purchases.Find(id);
            var clonePurchase = Organization.Purchases.Copy(purchase, DateTime.Now);
            return RedirectToAction("Home", "Purchase", new { id = clonePurchase.Uid });
        }



    }
}