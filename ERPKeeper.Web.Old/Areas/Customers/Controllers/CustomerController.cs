using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    [RouteArea("Customers")]
    [RoutePrefix("Customer")]
    [Route("{id}/{action=Home}")]
    public class CustomerController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.Customers.Find(id));
        public ActionResult Sales(Guid id) => View(Organization.Customers.Find(id));
        public ActionResult SaleQuotes(Guid id) => View(Organization.Customers.Find(id));
        public ActionResult MarkInActive(Guid id)
        {
            var customer = Organization.Customers.Find(id);
            customer.Status = ERPKeeper.Node.Models.Customers.Enums.CustomerStatus.InActive;
            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult MarkActive(Guid id)
        {
            var customer = Organization.Customers.Find(id);
            customer.Status = ERPKeeper.Node.Models.Customers.Enums.CustomerStatus.Active;
            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

     

        public ActionResult Delete(Guid id)
        {
            var profile = Organization.Customers.Find(id);
            if (profile != null)
            {
                Organization.Customers.Delete(profile);
                return RedirectToAction("Home", "Customers");
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public PartialViewResult _ChartIncome(Guid id)
        {
            DateTime StartDate = DateTime.Now.AddDays(-365);

            var SalesTables = Organization.Sales.GetQueryable()
                            .Where(t => t.ProfileGuid == id)
                            .Where(t => t.TrnDate > StartDate)
                            .GroupBy(t => new { t.TrnDate })
                            .Select(go => new
                            {
                                IncomeAmount = go.Where(tr => tr.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Sale).Sum(i => i.Total),
                                TrnDate = go.Key.TrnDate
                            })
                            .ToList();
            return PartialView("_ChartIncome", SalesTables);
        }
        public ActionResult NewQuote(Guid id)
        {
            var customerProfile = Organization.Profiles.Find(id);
            var salesQuote = Organization.SaleEstimates.Create(customerProfile, DateTime.Now, CurrentUser.ProfileUid);

            return RedirectToAction("Home", "Quote", new { id = salesQuote.Uid });
        }
        public ActionResult NewSale(Guid id)
        {
            var customerProfile = Organization.Profiles.Find(id);
            var sale = Organization.Sales.Create(customerProfile, DateTime.Now, CurrentUser.ProfileUid);

            return RedirectToAction("Home", "Sale", new { id = sale.Uid });
        }
        public ActionResult NewReceivePayment(Guid id)
        {
            var profile = Organization.Profiles.Find(id);
            var payment = Organization.ReceivePayments.Create(profile, null, DateTime.Now);
            return RedirectToAction("Home", "ReceivePayment", new { id = payment.Uid });
        }
        public PartialViewResult PartialGridViewSalesHistory(Guid id)
        {
            var salesHistoriesList = Organization.Sales.ListByProfile(id);

            ViewBag.id = id;
            return PartialView(salesHistoriesList);
        }
        public PartialViewResult PartialGridViewSaleQuotes(Guid id)
        {
            var salesHistoriesList = Organization.SaleEstimates.ListByProfile(id);
            ViewBag.id = id;
            return PartialView(salesHistoriesList);
        }


        public ActionResult UpdateAssistItems(Guid id)
        {
            Organization.AssistItems.Create(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}