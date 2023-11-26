using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Advertising
{
    public class AdvertisingAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Advertising";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                 name: "Advertising_default",
                url: "Advertising/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}