using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Locations.Controllers
{
    public class LocationsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult New()
        {
            var newLocation = new ERPKeeper.Node.Models.Location()
            {
                Uid = Guid.NewGuid(),
                Name = "Default Location",
                Type = Node.Models.LocationType.Building
            };
            Organization.Locations.Create(newLocation);

            return Redirect($"/Locations/Location/{newLocation.Uid}/Home");
        }
    }
}