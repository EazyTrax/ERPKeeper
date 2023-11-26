using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Profiles.Controllers
{
    [Route("/Profiles/{ProfileUid:Guid}/{action=Index}")]
    public class ProfileController : Base_ProfilesController
    {
        public Guid ProfileUid => Guid.Parse(RouteData.Values["ProfileUid"].ToString());

        public IActionResult Index()
        {
            var customer = Organization.Profiles.Find(ProfileUid);
            return View(customer);
        }
        public IActionResult Addresses()
        {
            var customer = Organization.Profiles.Find(ProfileUid);
            return View(customer);
        }
        public IActionResult Contacts()
        {
            var customer = Organization.Profiles.Find(ProfileUid);
            return View(customer);
        }
        public IActionResult Roles()
        {
            var customer = Organization.Profiles.Find(ProfileUid);
            return View(customer);
        }


        [HttpPost]
        public IActionResult Update(ERPKeeper.Node.Models.Profiles.Profile model)
        {
            var profile = Organization.Profiles.Find(ProfileUid);

            profile.Name = model.Name;
            profile.ShotName = model.ShotName;
            profile.TaxNumber = model.TaxNumber;
            profile.WebSite = model.WebSite;
            profile.PhoneNumber = model.PhoneNumber;
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
