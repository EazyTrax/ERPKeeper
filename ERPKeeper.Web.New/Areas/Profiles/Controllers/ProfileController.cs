using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;
using Z.EntityFramework.Plus;
using ERPKeeperCore.Enterprise.Models.Profiles;

namespace ERPKeeperCore.Web.Areas.Profiles.Controllers
{
    [Route("/{CompanyId}/Profiles/{ProfileUid:Guid}/{action=Index}")]
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
        public IActionResult AssignRole(string role)
        {
            var profile = Organization.Profiles.Find(ProfileUid);

            switch (role)
            {
                case "Customer":
                    Organization.Customers.Add(profile);
                    return Redirect($"/{CompanyId}/Customers/Customers/{profile.Id}");
                case "Supplier":
                    Organization.Suppliers.Add(profile);
                    return Redirect($"/{CompanyId}/Suppliers/Suppliers/{profile.Id}");
                case "Employee":
                    Organization.Employees.Add(profile);
                    return Redirect($"/{CompanyId}/Employees/Employeess/{profile.Id}");
                case "Investor":
                    Organization.Customers.Add(profile);
                    return Redirect($"/{CompanyId}/Investors/Investors/{profile.Id}");
            }


            return Redirect(Request.Headers["Referer"].ToString());
        }


        [HttpPost]
        public IActionResult Update(ERPKeeperCore.Enterprise.Models.Profiles.Profile model)
        {
            var profile = Organization.Profiles.Find(ProfileUid);

            profile.Name = model.Name;
            profile.ShotName = model.ShotName;
            profile.TaxNumber = model.TaxNumber;
            profile.WebSite = model.WebSite;
            profile.PhoneNumber = model.PhoneNumber;
            profile.IsSelfOrganization = model.IsSelfOrganization;

            Organization.SaveChanges();

            if (profile.IsSelfOrganization)
            {
                Console.WriteLine("IsSelfOrganization");

                Organization.ErpCOREDBContext.Profiles
                    .Where(p => p.Id != profile.Id)
                    .Update(p => new Profile { IsSelfOrganization = false });

                Organization.SaveChanges();
            }
            Console.WriteLine(profile.IsSelfOrganization);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
