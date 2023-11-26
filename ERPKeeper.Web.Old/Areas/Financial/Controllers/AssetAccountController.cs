using ERPKeeper.Node.Models.Accounting;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("AssetAccount")]
    [Route("{id}/{action=Home}")]
    public class AssetAccountController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(Guid id)
        {
            var account = Organization.ChartOfAccount.Find(id);
            return View(account);
        }

        public ActionResult Save(AccountItem accountItem)
        {
            var existBankAccount = Organization.ChartOfAccount.Find(accountItem.Uid);

            existBankAccount.BankAccountBankName = accountItem.BankAccountBankName;
            existBankAccount.BankAccountNumber = accountItem.BankAccountNumber;
            existBankAccount.BankAccountBranch = accountItem.BankAccountBranch;

            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);

        }



    }
}