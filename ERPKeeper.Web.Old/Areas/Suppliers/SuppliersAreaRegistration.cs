using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers
{
    public class SuppliersAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Suppliers";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              name: "Suppliers_default",
              url: "Suppliers/{controller}/{action}/{id}",
              defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
             );

        }
    }
}