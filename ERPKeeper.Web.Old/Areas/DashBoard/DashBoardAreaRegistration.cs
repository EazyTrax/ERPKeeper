using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.DashBoard
{
    public class DashBoardAreaRegistration : AreaRegistration
    {
        public override string AreaName => "DashBoard";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "DashBoard_default",
                url: "DashBoard/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
               );

            context.MapRoute(
              name: "DashBoard_Node_default",
              url: string.Empty,
              defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
             );



        }
    }
}