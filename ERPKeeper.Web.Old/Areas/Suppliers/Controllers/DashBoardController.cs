using ERPKeeper.Node.Models.Transactions;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{
    public class DashBoardController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();


        public PartialViewResult _ChartExpense(int duration = 90, double width = 269)
        {
            ViewBag.Width = width;
            ViewBag.Duration = duration;

            var dailyBalances = Organization.Purchases.ListDailyBalances(duration);




            return PartialView("_ChartExpense", dailyBalances);
        }

        public PartialViewResult _Chart(int duration = 365)
        {
            var dailyBalances = Organization.Purchases.ListDailyBalances(duration);
            return PartialView("_Chart", dailyBalances);
        }

        public PartialViewResult _ChartFlow()
        {

            return PartialView();
        }

        public PartialViewResult _ChartAge()
        {
            DateTime StartDate = DateTime.Today.AddDays(-90);
            var PurchasesTables = Organization.Purchases.ListOpen
                            .Where(t => t.TrnDate > StartDate)
                            .Where(t => t.Status == CommercialStatus.Open)
                            .GroupBy(t => new { t.TrnDate })
                            .Select(go => new PurchaseHistory
                            {
                                Amount = go.Where(tr => tr.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Purchase).Sum(i => i.Total),
                                TrnDate = go.Key.TrnDate
                            })
                            .OrderBy(it => it.TrnDate)
                            .ToList();


            decimal TotalAmount = 0;
            PurchasesTables.ForEach(it =>
            {
                TotalAmount = TotalAmount + it.Amount;
                it.TotalAmount = TotalAmount;
            });


            return PartialView("_ChartAge", PurchasesTables);
        }

    }

    public class PurchaseHistory
    {
        public DateTime TrnDate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal TotalAmount { get; set; }
    }
}