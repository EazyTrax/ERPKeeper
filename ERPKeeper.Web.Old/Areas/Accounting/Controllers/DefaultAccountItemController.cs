using ERPKeeper.Node.Models.Accounting.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{

    public class DefaultAccountItemController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(SystemAccountType id)
        {
            var account = Organization.SystemAccounts.GetAccount(id);
            return View(account);
        }

        //public ActionResult Home(DefaultAccountItem defaultAccountItem)
        //{
        //  var exitItem = organization.SystemAccounts.Find(defaultAccountItem.AccountType);

        //  if (exitItem != null)
        //    exitItem.AccountItemUid = defaultAccountItem.AccountItemUid;
        //  else
        //    organization.SystemAccounts.Add(defaultAccountItem);

        //  organization.SaveChanges();

        //  return RedirectToAction("Home", new { id = defaultAccountItem.AccountType });
        //}


    }
}