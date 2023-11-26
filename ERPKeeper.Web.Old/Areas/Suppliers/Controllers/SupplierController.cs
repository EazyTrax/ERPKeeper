using ERPKeeper.Node.Models.Suppliers.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{
    public class SupplierController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.Suppliers.Find(id));
        public ActionResult Purchases(Guid id) => View(Organization.Suppliers.Find(id));
        public ActionResult PurchaseQuotes(Guid id) => View(Organization.Suppliers.Find(id));

        public ActionResult MarkInActive(Guid id)
        {
            var supplier = Organization.Suppliers.Find(id);
            supplier.Status = SupplierStatus.InActive;
            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult MarkActive(Guid id)
        {
            var supplier = Organization.Suppliers.Find(id);
            supplier.Status = SupplierStatus.Active;
            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult Add(Guid id)
        {
            var profile = Organization.Profiles.Find(id);
            var newSupplier = Organization.Suppliers.Create(profile);
            return RedirectToAction("Home", "Supplier", new { id = newSupplier.ProfileUid, Area = "Suppliers" });
        }

        public ActionResult Save(ERPKeeper.Node.Models.Suppliers.Supplier supplier)
        {
            var ExSupplier = Organization.Suppliers.Find(supplier.ProfileUid);
            ExSupplier.Update(supplier);
            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public PartialViewResult _ChartExpense(Guid id)
        {
            DateTime StartDate = DateTime.Today.AddDays(-365);

            var SalesTables = Organization.Purchases.GetQueryable()
                            .Where(c => c.ProfileGuid == id)
                            .Where(t => t.TrnDate > StartDate)
                            .GroupBy(t => new { t.TrnDate })
                            .Select(go => new
                            {
                                ExpenseAmount = go.Sum(i => i.Total),
                                TrnDate = go.Key.TrnDate
                            })
                            .ToList();
            return PartialView("_ChartExpense", SalesTables);
        }





        public ActionResult NewPurchase(Guid id)
        {
            var purchase = Organization.Purchases.Create(id, DateTime.Today);
            return RedirectToAction("Home", "Purchase", new { id = purchase.Uid });
        }





        public ActionResult NewQuote(Guid id)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Create(id, DateTime.Today);

            return RedirectToAction("Home", "Quote", new { id = purchaseQuote.Uid });
        }

        public ActionResult NewSupplierPayment(Guid id)
        {
            var profile = Organization.Profiles.Find(id);
            var payment = Organization.SupplierPayments.Create(profile, null, DateTime.Today);
            return RedirectToAction("Home", "SupplierPayment", new { id = payment.Uid });
        }




        public PartialViewResult PartialGridViewPurchaseHistory(Guid id)
        {
            var transactions = Organization.Purchases.GetQueryable()
                .Where(Transaction => Transaction.Profile.Uid == id)
                .ToList();

            ViewBag.id = id;

            return PartialView(transactions);
        }

        public ActionResult UpdateAssistItems(Guid id)
        {
            Organization.AssistItems.Create(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}