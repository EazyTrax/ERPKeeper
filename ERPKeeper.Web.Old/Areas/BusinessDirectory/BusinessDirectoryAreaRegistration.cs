using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.BusinessDirectory
{
    public class BusinessDirectoryAreaRegistration : AreaRegistration
    {
        public override string AreaName => "BusinessDirectory";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "BusinessDirectory_default",
               "BusinessDirectory/{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
           );
        }
    }
}