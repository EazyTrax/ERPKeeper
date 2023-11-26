using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.NewsFeed
{
    public class NewsFeedAreaRegistration : AreaRegistration
    {
        public override string AreaName => "NewsFeed";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "NewsFeed_default",
                url: "NewsFeed/{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}