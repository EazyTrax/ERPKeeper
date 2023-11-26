using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Transactions
{
    public class TransactionsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Transactions";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            name: "Transactions_default",
            url: "Transactions/{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
           );
        }
    }
}