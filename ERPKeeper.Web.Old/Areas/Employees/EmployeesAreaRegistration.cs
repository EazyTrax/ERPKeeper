using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Employees
{
    public class EmployeesAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Employees";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              name: "Employees_defaultId",
              url: "Employees/{controller}/{action}/{id}",
              defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
             );
        }
    }
}