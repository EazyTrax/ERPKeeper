using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Customers
{
    public class CustomersAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Customers";

        public override void RegisterArea(AreaRegistrationContext context)
        {



            context.MapRoute(
                name: "Customers_default",
                url: "Customers/{controller}/{action}/{id}",
                defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}