using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Financial
{
    public class FinancialAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Financial";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               name: "Financial_default",
               url: "Financial/{controller}/{action}/{id}",
               defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
              );
        }
    }
}