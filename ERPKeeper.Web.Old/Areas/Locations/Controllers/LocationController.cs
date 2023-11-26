using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Locations.Controllers
{
    [RouteArea("Locations")]
    [Route("Location/{id}/{action=Home}")]
    public class LocationController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var location = Organization.Locations.Find(id);
            return View(location);
        }

        [HttpPost]
        public ActionResult Save(Node.Models.Location model)
        {
            var location = Organization.Locations.Find(model.Uid);

            if (location != null)
            {
                location.Name = model.Name;
                location.Type = model.Type;
                Organization.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult NewChild(Guid id)
        {
            var location = Organization.Locations.Find(id);
            var newLocation = new ERPKeeper.Node.Models.Location()
            {
                Uid = Guid.NewGuid(),
                Name = "Child of " + location.Name,
                Type = Node.Models.LocationType.Building,
                ParentUid = id
            };
            Organization.Locations.Create(newLocation);

            return Redirect($"/Locations/Location/{newLocation.Uid}/Home");
        }
    }
}