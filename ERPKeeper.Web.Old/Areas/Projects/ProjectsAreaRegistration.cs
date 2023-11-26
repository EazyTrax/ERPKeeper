using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Projects
{
    public class ProjectsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "projects";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Projects_default",
               url: "projects/{controller}/{action}/{id}",
               defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}