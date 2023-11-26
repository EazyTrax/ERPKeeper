using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("LiabilityAccount")]
    [Route("{id}/{action=Home}")]
    public class LiabilityAccountController : WebFrontEnd.Controllers._DefaultNodeController
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


        public ActionResult Payments(Guid id)
        {
            var account = Organization.ChartOfAccount.Find(id);
            return View(account);
        }


        public PartialViewResult PartialGridViewPayments(Guid id)
        {
            var models = Organization.ErpNodeDBContext.LiabilityPayments
                .Where(lp => lp.LiabilityAccountUid == id)
                .ToList();
            return PartialView(models);
        }



        public ActionResult Save(ERPKeeper.Node.Models.Accounting.AccountItem accountItem)
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