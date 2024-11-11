
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Enterprise_Setting.Controllers
{

    public class BranchesController : _Enterprise_Setting_BaseController
    {
        public IActionResult Index()
        {
            var branch = Organization.ErpCOREDBContext.Branches.FirstOrDefault();

            if (branch == null)
            {
                var self = Organization.Profiles.GetSelf();
                var firstBranchAddresse = self.Addresses.First();

                branch = new Enterprise.Models.Setting.Branch()
                {
                    OrganizationName = self.Name,
                    BranchName = firstBranchAddresse.Name ?? "สำนักงานใหญ่",
                    PhoneNumber = firstBranchAddresse.PhoneNumber ?? "0863584160",
                    Address = firstBranchAddresse.AddressLine,
                    No = Organization.ErpCOREDBContext.Branches.Count() + 1,
                 
                };

                Organization.ErpCOREDBContext.Branches.Add(branch);
                Organization.ErpCOREDBContext.SaveChanges();
            }

            return View();
        }

    }
}
