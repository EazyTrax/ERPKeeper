using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Commercials
{
    public class CommercialsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Commercials";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Commercials_default",
                url: "Commercials/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );


        }
    }
}