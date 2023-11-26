using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Setting
{
    public class SettingAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Setting";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               name: "Setting_default",
               url: "Setting/{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
              );
        }
    }
}