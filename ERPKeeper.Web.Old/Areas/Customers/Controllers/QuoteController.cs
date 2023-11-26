using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Estimations;
using ERPKeeper.Node.Models.Estimations.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    [RouteArea("Customers")]
    [Route("Quote/{id}/{action=Home}")]

    public class QuoteController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.SaleEstimates.Find(id));


        public ActionResult ConvertToOrder(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);

            if (saleQuote.Status != QuoteStatus.Quote)
                return Content($"{saleQuote.Name} Status must be {QuoteStatus.Quote} only.");
            else
            {
                saleQuote.Status = QuoteStatus.Ordered;
                saleQuote.OrderDate = DateTime.Now;
                Organization.SaveChanges();
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
        }


        public RedirectResult Claim(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            saleQuote.ResponsibleGuid = CurrentUser.ProfileUid;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public RedirectResult ConvertToOpen(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            saleQuote.Status = QuoteStatus.Quote;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public RedirectResult ConvertToVoid(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            saleQuote.Status = QuoteStatus.Void;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult ConvertToSale(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            if (saleQuote.Status != QuoteStatus.Ordered)
                return Content($"{saleQuote.Name} Status must be {QuoteStatus.Ordered} only.");
            else
            {
                saleQuote.CloseDate = DateTime.Now;
                saleQuote.Status = QuoteStatus.Close;

                var newSale = Organization.Sales.Create(saleQuote.Profile, DateTime.Now);
                newSale.ProjectGuid = saleQuote.ProjectGuid;
                newSale.PaymentTermGuid = saleQuote.PaymentTermGuid;
                newSale.Reference = saleQuote.Reference;
              

                if (saleQuote.TaxCode != null)
                    newSale.TaxCode = saleQuote.TaxCode;

                saleQuote.Items.OrderBy(i => i.Order).ToList().ForEach(estimateItem =>
                  {
                      var commercialItem = newSale.AddItem(estimateItem.Item, (int)estimateItem.Amount);
                      commercialItem.ItemPartNumber = estimateItem.ItemPartNumber;
                      commercialItem.ItemDescription = estimateItem.ItemDescription;
                      commercialItem.UnitPrice = estimateItem.UnitPrice;
                      commercialItem.Order = estimateItem.Order;
                  });

                newSale.ReCalculate();
                Organization.SaveChanges();

                return RedirectToAction("Home", "Sale", new { Area = "Customers", id = newSale.Uid });
            }
        }

        public ActionResult Items(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            return View(saleQuote);
        }

        public PartialViewResult _AssistItems(Guid id)
        {
            ViewBag.Id = id;
            var estimate = Organization.SaleEstimates.Find(id);
            var assistItems = Organization.AssistItems.GetItems(estimate.ProfileGuid);

            return PartialView(assistItems);
        }

        public PartialViewResult _SummaryCallbackPanel(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            return PartialView(saleQuote);
        }

        public PartialViewResult PartialGridViewItems(Guid id)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            return PartialView(saleQuote);
        }

        public PartialViewResult PartialGridViewItemsUpdate(QuoteItem estimateItem)
        {
            var existQuoteItem = Organization.QuoteItems.Find(estimateItem.Uid);
            var salesQuote = existQuoteItem.Quote;

            try
            {
                if (estimateItem.Amount == 0)
                    Organization.QuoteItems.Remove(existQuoteItem);
                else
                    existQuoteItem.Update(estimateItem);

                salesQuote.Calculate();
                salesQuote.ReOrder();
                Organization.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }


            return PartialView("PartialGridViewItems", salesQuote);
        }

        public PartialViewResult Partial_ComboBoxItems()
        {
            var items = Organization.Items.GetItems(ERPObjectType.Sale);
            return PartialView(items);
        }
        public ActionResult AddItem(Guid id, Guid itemUid, int Amount = 1)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            var addedItem = Organization.Items.Find(itemUid);

            saleQuote.AddItem(addedItem, Amount);
            saleQuote.Calculate();

            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Documents(Guid id)
        {
            ViewBag.id = id;
            var _Transaction = Organization.SaleEstimates.Find(id);
            return View(_Transaction);
        }
        public ActionResult Save(SaleQuote salesQuote)
        {
            Organization.SaleEstimates.UpdateChanges(salesQuote);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Recalculate()
        {
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult ChangeAddress(Guid id, Guid addressGuid)
        {
            var saleQuote = Organization.SaleEstimates.Find(id);
            saleQuote.ProfileAddressGuid = addressGuid;

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Delete(Guid id)
        {
            Organization.SaleEstimates.Delete(id);
            return RedirectToAction("Home", "Quotes");
        }
        public ActionResult Copy(Guid id)
        {
            var SaleQuote = Organization.SaleEstimates.Find(id);
            var cloneSaleQuote = Organization.SaleEstimates.Copy(SaleQuote, DateTime.Now);
            return RedirectToAction("Home", "Quote", new { id = cloneSaleQuote.Uid });
        }
        public ActionResult ResetPayment(Guid id)
        {
            var salesQuote = Organization.SaleEstimates.Find(id);

            salesQuote.PaymentDate = null;
            salesQuote.AssetAccountUid = null;
            salesQuote.Status = ERPKeeper.Node.Models.Estimations.Enums.QuoteStatus.Ordered;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);

        }

        public ActionResult Export(Guid id, string DocumentType = "Quote")
        {

            var salesQuote = Organization.SaleEstimates.Find(id);
            salesQuote.UpdateName();
            Organization.SaveChanges();

            var salesQuotes = Organization.SaleEstimates.Query().Where(tr => tr.Uid == id).ToList();
            Node.Reports.Customers.Quote report = Organization.SaleEstimates.Export(id, DocumentType, CurrentUser.Name);

            ViewBag.Report = report;
            return View(salesQuotes.FirstOrDefault());
        }


        [AllowAnonymous]
        public ActionResult ExportGuest(Guid id, string DocumentType = "Quote")
        {
            var salesQuotes = Organization.SaleEstimates.Query().Where(tr => tr.Uid == id).ToList();
            Node.Reports.Customers.Quote report = Organization.SaleEstimates.Export(id, DocumentType, "Guest");

            ViewBag.Report = report;
            return View(salesQuotes.FirstOrDefault());
        }


    }
}