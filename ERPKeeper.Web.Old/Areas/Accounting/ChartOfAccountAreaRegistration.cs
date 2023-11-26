using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting
{
    public class AccountingAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Accounting";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              name: "Accounting_default",
              url: "Accounting/{controller}/{action}/{id}",
              defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}