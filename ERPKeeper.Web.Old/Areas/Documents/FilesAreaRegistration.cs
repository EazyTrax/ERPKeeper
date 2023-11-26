using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Documents
{
    public class DocumentsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Documents";

        public override void RegisterArea(AreaRegistrationContext context)
        {


            context.MapRoute(
             name: "Documents_default",
             url: "Documents/{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}