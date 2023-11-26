using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Tasks
{
    public class TasksAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Tasks";
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Tasks_default",
               url: "Tasks/{controller}/{action}/{id}",
               defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}