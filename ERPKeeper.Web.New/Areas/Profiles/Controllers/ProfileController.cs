using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles.Controllers
{
    [Route("/{CompanyId}/Profiles/{ProfileUid:Guid}/{action=Index}")]
    public class ProfileController : Base_ProfilesController
    {
        public Guid ProfileUid => Guid.Parse(RouteData.Values["ProfileUid"].ToString());

        public IActionResult Index()
        {
            var customer = OrganizationCore.Profiles.Find(ProfileUid);
            return View(customer);
        }
        public IActionResult Addresses()
        {
            var customer = OrganizationCore.Profiles.Find(ProfileUid);
            return View(customer);
        }
        public IActionResult Contacts()
        {
            var customer = OrganizationCore.Profiles.Find(ProfileUid);
            return View(customer);
        }
        public IActionResult Roles()
        {
            var customer = OrganizationCore.Profiles.Find(ProfileUid);
            return View(customer);
        }


        [HttpPost]
        public IActionResult Update(ERPKeeperCore.Enterprise.Models.Profiles.Profile model)
        {
            var profile = OrganizationCore.Profiles.Find(ProfileUid);

            profile.Name = model.Name;
            profile.ShotName = model.ShotName;
            profile.TaxNumber = model.TaxNumber;
            profile.WebSite = model.WebSite;
            profile.PhoneNumber = model.PhoneNumber;
            OrganizationCore.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
