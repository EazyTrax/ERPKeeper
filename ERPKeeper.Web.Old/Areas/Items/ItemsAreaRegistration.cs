using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Items
{
    public class ItemsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Items";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Items_default",
                "Items/{controller}/{action}/{id}",
                 defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );


        }
    }
}