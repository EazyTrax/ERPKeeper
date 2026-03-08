using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ERPKeeper.Web.New.Areas.Setting.Models;

namespace ERPKeeperCore.Web.Areas.Setting.Controllers
{
    public class PasswordController : _SettingBaseController
    {
        public IActionResult Index()
        {
            var member = _dbContext.Profiles.Find(AuthorizeUserId);

            if (member == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ChangePasswordViewModel model)
        {
            var member = _dbContext.Profiles.Find(AuthorizeUserId);
            if (member == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Verify current password
            if (member.Password != model.CurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Current password is incorrect");
                return View(model);
            }

            // Update password
            member.Password = model.NewPassword;
            _dbContext.SaveChanges();

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("Index");
        }
    }
}
