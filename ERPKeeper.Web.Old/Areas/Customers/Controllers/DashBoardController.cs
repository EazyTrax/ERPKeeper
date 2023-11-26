using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    public class DashBoardController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult _TopCustomers() => PartialView();
        public PartialViewResult _ChartIncome(int duration = 90, double width = 269)
        {
            ViewBag.Width = width;
            ViewBag.Duration = duration;

            var dailyBalances = Organization.Sales.ListDailyBalances(duration);
            return PartialView("_ChartIncome", dailyBalances);
        }
        public PartialViewResult _Chart(int duration = 365)
        {
            var dailyBalances = Organization.Sales.ListDailyBalances(duration);
            return PartialView("_Chart", dailyBalances);
        }
        public PartialViewResult _ChartFlow()
        {
            return PartialView();
        }

        public PartialViewResult _ChartAge()
        {
            DateTime StartDate = DateTime.Now.AddDays(-90);

            var SalesTables = Organization.Sales.GetQueryable()
                            .Where(t => t.TrnDate > StartDate)
                            .Where(t => t.Status == ERPKeeper.Node.Models.Transactions.CommercialStatus.Open)
                            .GroupBy(t => new { t.TrnDate })
                            .Select(go => new SaleHistory
                            {
                                Amount = go.Sum(i => i.Total),
                                TrnDate = go.Key.TrnDate
                            })
                            .OrderBy(it => it.TrnDate)
                            .ToList();


            decimal TotalAmount = 0;
            SalesTables.ForEach(it =>
            {
                TotalAmount = TotalAmount + it.Amount;
                it.TotalAmount = TotalAmount;
            });

            return PartialView("_ChartAge", SalesTables);
        }
    }
}