using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    public class DefaultAccountsController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home()
        {
            return View();
        }

        public PartialViewResult PartialGridView()
        {
            return PartialView(Organization.SystemAccounts.GetAll());
        }
        public PartialViewResult PartialGridViewUpdate(ERPKeeper.Node.Models.Accounting.DefaultAccountItem defaultItem)
        {
            var existSystemAccount = Organization.SystemAccounts.Find(defaultItem.AccountType);

            if (existSystemAccount != null)
            {
                existSystemAccount.AccountItemUid = defaultItem.AccountItemUid;
                Organization.SaveChanges();
            }

            return PartialView("PartialGridView", Organization.SystemAccounts.GetAll());
        }




        public ActionResult AutoAssign()
        {
            Organization.SystemAccounts.AutoAssignSystemAccount();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}