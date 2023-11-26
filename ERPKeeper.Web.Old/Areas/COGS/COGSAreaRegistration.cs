using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.COGS
{
    public class ProjectsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "COGS";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "COGS_default",
               url: "{Node}/COGS/{controller}/{action}/{id}",
               defaults: new {  controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}