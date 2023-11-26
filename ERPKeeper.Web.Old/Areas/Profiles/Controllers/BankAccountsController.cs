using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Profiles.Controllers
{

    public class BankAccountsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        public ActionResult Home(Guid id)
        {

            var profile = Organization.Profiles.Find(id);

            if (profile != null)
            {
                return View(profile);
            }
            else
            {
                return RedirectToAction("Home", "Profiles");
            }

        }

        public PartialViewResult _BankAccounts(Guid id)
        {
            var bankAccounts = Organization.Profiles.Find(id);

            return PartialView(bankAccounts);
        }
    }
}