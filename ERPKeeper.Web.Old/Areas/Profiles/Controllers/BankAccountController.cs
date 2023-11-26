using ERPKeeper.Node.Models.Profiles;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Profiles.Controllers
{
    [RouteArea("Profiles")]
    [RoutePrefix("BankAccount")]
    [Route("{id}/{action=Home}")]
    public class BankAccountController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.ProfileBankAccounts.Find(id));

        public ActionResult Save(ProfileBankAccount bankAccount)
        {
            var existBankAccount = Organization.ProfileBankAccounts.Find(bankAccount.BankAccountGuid);
            existBankAccount.Update(bankAccount);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult New(Guid profileGuid)
        {
            var bankAccount = Organization.ProfileBankAccounts.CreateNew(profileGuid);
            return RedirectToAction("Home", new { id = bankAccount.BankAccountGuid });

        }
    }
}