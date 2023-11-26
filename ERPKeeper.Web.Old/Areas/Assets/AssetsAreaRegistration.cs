using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Assets
{
    public class AssetsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Assets";


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Assets_default",
                url: "Assets/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );


        }
    }
}