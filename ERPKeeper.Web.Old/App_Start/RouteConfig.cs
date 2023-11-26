using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.Ignore("{resource}.ashx/{*pathInfo}");
            routes.Ignore("favicon.ico");

            routes.MapMvcAttributeRoutes();




            //routes.MapMvcAttributeRoutes();

            // routes.MapRoute(
            //  name: "Root_Default", // Route name
            //  url: "", // URL with parameters
            //  defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional },
            //  namespaces: new[] { "ERPKeeper.WebFrontEnd.Controllers" }
            //);


            //  routes.MapRoute(
            //   name: "Root_Home_Default", // Route name
            //   url: "Home/{action}/{id}", // URL with parameters
            //   defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional },
            //   namespaces: new[] { "ERPKeeper.WebFrontEnd.Controllers" }
            // );


            //  routes.MapRoute(
            //  name: "Default", // Route name
            //  url: "icon/{action}/{id}", // URL with parameters
            //  defaults: new { controller = "icon", action = "Home", id = UrlParameter.Optional },
            //  namespaces: new[] { "ERPKeeper.WebFrontEnd.Controllers" }
            //);
        }
    }
}