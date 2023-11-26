using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Taxes
{
    public class TaxesAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Taxes";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
             name: "Taxes_default",
             url: "Taxes/{controller}/{action}/{id}",
             defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}