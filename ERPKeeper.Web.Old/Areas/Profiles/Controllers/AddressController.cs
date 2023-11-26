using ERPKeeper.Node.Models.Profiles;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Profiles.Controllers
{

    public class AddressController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.ProfileAddresses.Find(id));


        public ActionResult New(Guid profileGuid)
        {
            var addreses = Organization.ProfileAddresses.CreateNew(profileGuid);

            return RedirectToAction("Home", new { id = addreses.AddressGuid });
        }

        public ActionResult Save(ProfileAddress address)
        {
            var existAddress = Organization.ProfileAddresses.Find(address.AddressGuid);

            if (existAddress != null)
                existAddress.Update(address);
            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = address.AddressGuid });
        }
    }
}