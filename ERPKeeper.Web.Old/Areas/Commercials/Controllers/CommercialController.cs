
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Transactions;
using ERPKeeper.WebFrontEnd.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Commercials.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]

    [RouteArea("Commercials")]
    [Route("Commercial/{id}/{action}")]
    public class CommercialController : _DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult RemoveTaxOffset(Guid id)
        {
            var transaction = Organization.Purchases.Find(id);
            if (transaction.PostStatus == LedgerPostStatus.Editable)
            {
                transaction.TaxOffset = 0;
                transaction.ReCalculate();
                Organization.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public PartialViewResult _Detail(Guid id)
        {
            var transaction = Organization.Commercials.Find(id);
            return PartialView(transaction);
        }
        public PartialViewResult _Payment(Guid id)
        {
            var transaction = Organization.Commercials.Find(id);
            return PartialView(transaction);
        }
        public ActionResult Redirect(Guid id, ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType? type)
        {
            string areaName = string.Empty;

            if (type == null)
            {
                var _Transaction = Organization.Commercials.Find(id);
                type = _Transaction.TransactionType;
            }
            else
            {

            }


            switch (type)
            {


                case ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.FixedAsset:
                    areaName = "Company";
                    break;
                case ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Sale:
                    areaName = "Customers";
                    break;
                case ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Purchase:
                    areaName = "Suppliers";
                    break;
                case ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.FundTransfer:

                case ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.EarnestPayment:
                case ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.EarnestReceive:
                    areaName = "Financial";
                    break;

                case ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.EmployeePayment:
                    areaName = "Employees";
                    break;
            }


            return RedirectToAction("Home", type.ToString(), new { id = id, Area = areaName });

        }
        public ActionResult ChangeStatus(Guid id, ERPKeeper.Node.Models.Transactions.CommercialStatus NewStatus)
        {
            Organization.Commercials.ChangeStatus(id, NewStatus);
            return RedirectToAction("Redirect", "Commercial", new { id = id });
        }
        public ActionResult ChangeAddress(Guid id, Guid addressGuid)
        {
            Organization.Commercials.ChangeAddress(id, addressGuid);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public PartialViewResult PartialGridViewItems(Guid id)
        {
            var transaction = Organization.Commercials.Find(id);
            return PartialView(transaction);
        }



        public PartialViewResult PartialGridViewItemsUpdate(CommercialItem commercialItem)
        {
            var existCommercialItem = Organization.CommercialItems.Find(commercialItem.Uid);
            var commercial = existCommercialItem.Commercial;

            try
            {
                if (commercialItem.Amount == 0)
                    commercial.CommercialItems.Remove(existCommercialItem);
                else
                    existCommercialItem.Update(commercialItem);

                commercial.ReCalculate();
                Organization.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }

            return PartialView("PartialGridViewItems", commercial);
        }
        public PartialViewResult _Summary(Guid id)
        {
            var commercial = Organization.Commercials.Find(id);

            if (commercial != null)
                return PartialView(commercial);
            else
                return null;
        }
        public PartialViewResult _SummaryCallbackPanel(Guid id)
        {
            var commercialTransaction = Organization.Commercials.Find(id);
            if (commercialTransaction != null)
            {
                return PartialView(commercialTransaction);
            }
            return null;
        }

        public PartialViewResult _AssistItems(Guid id)
        {
            ViewBag.CommercialId = id;
            var commercial = Organization.Commercials.Find(id);
            var assistItems = Organization.AssistItems.GetItems(commercial.ProfileGuid);

            return PartialView(assistItems);
        }

        public ActionResult UpdateAssistItems(Guid id)
        {
            Organization.AssistItems.Create(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [ValidateInput(false)]
        public PartialViewResult _JournalGridView(Guid id)
        {
            var model = Organization.LedgersDal.Query()
                .Where(journal => journal.TransactionGuid == id);

            var ledgers = model.OrderBy(journal => journal.TrnDate).ThenBy(journal => journal.accountItem.No).ToList();

            return PartialView("_JournalGridView", ledgers);
        }
        public ActionResult AddItem(Guid id, Guid itemUid, int Amount = 1)
        {
            var targetCommercial = Organization.Commercials.Find(id);

            if (targetCommercial.PostStatus == LedgerPostStatus.Editable)
            {
                var addedItem = Organization.Items.Find(itemUid);

                targetCommercial.AddItem(addedItem, Amount);
                targetCommercial.SortItemsOrder();
                targetCommercial.ReCalculate();

                Organization.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult CreateProject(Guid id)
        {
            var commercial = Organization.Commercials.Find(id);
            var project = Organization.Projects.Create("New Project", "N/A");
            commercial.ProjectGuid = project.Uid;

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult AddShipment(Guid id)
        {
            var targetCommercial = Organization.Commercials.Find(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Recalculate(Guid id)
        {
            var commercial = Organization.Commercials.Find(id);

            commercial.UpdatePaymentStatus();
            Organization.SaveChanges();

            if (commercial == null)
                return Content("Cannot Find commercial. ");
            else if (commercial.PostStatus == LedgerPostStatus.Locked)
                return Content("Ledger Posted!. ");
            else
                commercial.ReCalculate();

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}