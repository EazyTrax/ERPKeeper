using ERPKeeper.Node.Models.Company;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Setup.Controllers
{
    [AllowAnonymous]
    public class HomeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        public ActionResult Home()
        {
            if (Organization.SelfProfile != null)
                return RedirectToAction("Home", "Home", new { Area = "DashBoard" });

            var newCompanyModel = new NewCompanyModel()
            {
                FirstDate = new DateTime(DateTime.Now.Year, 1, 1)
            };

            return View(newCompanyModel);
        }

        public ActionResult CreateCompany(NewCompanyModel newCompany)
        {
            Organization.CreateCompanyDB(newCompany);

            return RedirectToAction("Home", "Home", new { Area = "DashBoard" });
        }
    }
}