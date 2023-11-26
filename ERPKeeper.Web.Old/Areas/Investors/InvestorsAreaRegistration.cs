using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Investors
{
    public class InvestorsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Investors";
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            name: "OwnerDefaultWithId",
            url: "Investors/{controller}/{action}/{id}",
            defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
           );
        }
    }
}