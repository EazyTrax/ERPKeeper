using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;
using Z.EntityFramework.Plus;
using ERPKeeperCore.Enterprise.Models.Profiles;
using ERPKeeper.Web.New.Areas.Profiles.Models;

namespace ERPKeeperCore.Web.Areas.Profiles_Profile.Controllers
{
    [Route("/Profiles/{ProfileUid:Guid}/{action=Index}")]
    public class HomeController : Profiles_Profile_BaseController
    {
       
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Addresses()
        {

            return View();
        }
        public IActionResult Contacts()
        {

            return View();
        }
        public IActionResult Roles()
        {
     
            return View();
        }

        // Change Password GET
        public IActionResult ChangePassword()
        {
            var profile = Organization.Profiles.Find(ProfileUid);
            if (profile == null)
            {
                return NotFound();
            }


            return View(profile);
        }

        // Change Password POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var profile = Organization.Profiles.Find(ProfileUid);
            if (profile == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Profile = profile;
                return View(model);
            }

            // Verify current password
            if (profile.Password != model.CurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Current password is incorrect");
                ViewBag.Profile = profile;
                return View(model);
            }

            // Update password
            profile.Password = model.NewPassword;
            Organization.SaveChanges();

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult AssignRole(string role)
        {

            switch (role)
            {
                case "Customer":
                    Organization.Customers.Add(Profile);
                    return Redirect($"/Customers/Customers/{Profile.Id}");
                case "Supplier":
                    Organization.Suppliers.Add(Profile);
                    return Redirect($"/Suppliers/Suppliers/{Profile.Id}");
                case "Employee":
                    Organization.Employees.Add(Profile);
                    return Redirect($"/HR/Employeess/{Profile.Id}");
                case "Investor":
                    Organization.Customers.Add(Profile);
                    return Redirect($"/Investors/Investors/{Profile.Id}");
            }


            return Redirect(Request.Headers["Referer"].ToString());
        }


        [HttpPost]
        public IActionResult Update(ERPKeeperCore.Enterprise.Models.Profiles.Profile model)
        {


            Profile.Name = model.Name;
            Profile.ShotName = model.ShotName;
            Profile.TaxNumber = model.TaxNumber;
            Profile.WebSite = model.WebSite;
            Profile.PhoneNumber = model.PhoneNumber;
            Profile.IsSelfOrganization = model.IsSelfOrganization;

            Organization.SaveChanges();

            if (Profile.IsSelfOrganization)
            {
                Console.WriteLine("IsSelfOrganization");

                Organization.ErpCOREDBContext.Profiles
                    .Where(p => p.Id != Profile.Id)
                    .Update(p => new ERPKeeperCore.Enterprise.Models.Profiles.Profile { IsSelfOrganization = false });

                Organization.SaveChanges();
            }
            Console.WriteLine(Profile.IsSelfOrganization);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
