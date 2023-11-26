using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Search
{
    public class SearchAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Search";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Search_default",
                url: "Search/{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}