using ERPKeeper.Node.DAL;
using ERPKeeper.Node.Models.Profiles;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Profiles.Controllers
{
    public class ProfilesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home() => RedirectToAction("Home", "DashBoard");


        public ActionResult People() => View("Home", ProfileViewType.People);
        public ActionResult Organizations() => View("Home", ProfileViewType.Organization);

        public PartialViewResult _PreloadProfilesGridView(ProfileViewType profileViewType) => PartialView(profileViewType);


        public PartialViewResult PartialGridView(ProfileViewType profileViewType)
        {
            ViewBag.ProfileViewType = profileViewType;

            var profiles = Organization
                .Profiles
                .GetProfiles(profileViewType)
                .ToList();

            return PartialView("PartialGridView", profiles);
        }


        public PartialViewResult _ComboBoxProfiles() => PartialView(Organization.Profiles.ListAll());


        public ActionResult Create(ProfileType Type = ProfileType.Organization)
        {
            var newProfile = Organization.Profiles.CreateNew(Type);

            return RedirectToAction("Home", "Profile", new { id = newProfile.Uid });
        }

        public ActionResult SearchFromRD(string TaxId)
        {
            Profile profile = null;

            if (TaxId != null)
                profile = Organization.Profiles.SearchByTaxNumber(TaxId, false);

            return View(profile);
        }

        public ActionResult CreateByTax(string TaxId)
        {
            var profile = Organization.Profiles.SearchByTaxNumber(TaxId, true);

            if (profile != null)
                return RedirectToAction("Home", "Profile", new { id = profile.Uid });
            else
                return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult RemoveUnReferenceUser()
        {
            Organization.Profiles.RemoveUnReferenceProfile();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult RdUpdate()
        {
            Organization.Profiles.RemoveUnReferenceProfile();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}