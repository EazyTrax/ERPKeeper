using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Estimations;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{

    [RouteArea("Suppliers")]
    [Route("Quote/{id}/{action=Home}")]
    public class QuoteController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(Guid id)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Find(id);
            return View(purchaseQuote);
        }

        public RedirectResult Order(Guid id)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Find(id);
            purchaseQuote.Status = Node.Models.Estimations.Enums.QuoteStatus.Ordered;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Void(Guid id)
        {
            var PurchaseQuote = Organization.PurchaseEstimates.Find(id);
            PurchaseQuote.Status = Node.Models.Estimations.Enums.QuoteStatus.Void;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        public ActionResult ConvertToPurchase(Guid id)
        {
            var estimate = Organization.PurchaseEstimates.Find(id);
            estimate.Status = Node.Models.Estimations.Enums.QuoteStatus.Close;
            Organization.SaveChanges();


            var purchase = Organization.Purchases.Create((Guid)estimate.ProfileGuid, DateTime.Today);
            purchase.ProjectGuid = estimate.ProjectGuid;
            purchase.PaymentTermGuid = estimate.PaymentTermGuid;
            purchase.Reference = estimate.Reference;
            Organization.SaveChanges();

            estimate.Items.ToList().ForEach(estimateItem =>
            {
                var commercialItem = purchase.AddItem(estimateItem.Item, (int)estimateItem.Amount);
                commercialItem.ItemPartNumber = estimateItem.ItemPartNumber;
                commercialItem.ItemDescription = estimateItem.ItemDescription;
                commercialItem.UnitPrice = estimateItem.UnitPrice;
            });
            estimate.Calculate();
            Organization.SaveChanges();

            return RedirectToAction("Home", "Purchase", new { Area = "Suppliers", id = purchase.Uid });
        }

        public ActionResult Items(Guid id)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Find(id);
            return View(purchaseQuote);
        }
        public PartialViewResult _SummaryCallbackPanel(Guid id)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Find(id);
            return PartialView(purchaseQuote);
        }



        public PartialViewResult PartialGridViewItems(Guid id)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Find(id);

            return PartialView(purchaseQuote);
        }

        public PartialViewResult PartialGridViewItemsUpdate(ERPKeeper.Node.Models.Estimations.QuoteItem estimateItem)
        {
            var existEstItem = Organization.QuoteItems.Find(estimateItem.Uid);
            var estimate = existEstItem.Quote;

            try
            {
                if (estimateItem.Amount == 0)
                    estimate.RemoveItem(existEstItem);
                else
                    existEstItem.Update(estimateItem);

                estimate.Calculate();
                estimate.ReOrder();
                Organization.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("PartialGridViewItems", estimate);
        }

        public PartialViewResult Partial_ComboBoxItems()
        {
            var items = Organization.Items.GetItems(ERPObjectType.Purchase);
            return PartialView(items);
        }


        public ActionResult AddItem(Guid id, Guid itemUid, int Amount = 1)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Find(id);
            var addedItem = Organization.Items.Find(itemUid);

            purchaseQuote.AddItem(addedItem, Amount);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Documents(Guid id)
        {
            ViewBag.id = id;
            var model = Organization.PurchaseEstimates.Find(id);
            return View(model);
        }


        public ActionResult Save(PurchaseQuote PurchasesQuote)
        {
            Organization.PurchaseEstimates.SaveChanges(PurchasesQuote);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult ChangeAddress(Guid id, Guid addressGuid)
        {
            var purchaseQuote = Organization.PurchaseEstimates.Find(id);
            purchaseQuote.ProfileAddressGuid = addressGuid;
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }




        public ActionResult Delete(Guid id)
        {
            Organization.PurchaseEstimates.Delete(id);
            return RedirectToAction("Home", "Quotes");
        }
     

        public ActionResult Copy(Guid id)
        {
            var PurchaseQuote = Organization.PurchaseEstimates.Find(id);
            var clonePurchaseQuote = Organization.PurchaseEstimates.Copy(PurchaseQuote, DateTime.Today);
            return RedirectToAction("Home", "PurchaseQuote", new { id = clonePurchaseQuote.Uid });
        }


        public ActionResult Export(Guid id, string DocumentType = "Quote")
        {
            var PurchaseQuotes = Organization.PurchaseEstimates.Query()
                .Where(tr => tr.Uid == id)
                .ToList();

            var Report = new Node.Reports.Suppliers.Quote()
            {
                DataSource = PurchaseQuotes,
                Name = id.ToString("D")
            };


            Report.Parameters["DocumentType"].Value = DocumentType;
            Report.Parameters["DocumentType"].Visible = false;

            ViewBag.Report = Report;
            return View(PurchaseQuotes.First());
        }

    }
}