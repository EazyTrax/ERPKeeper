using ERPKeeper.Node.Models.Accounting;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    public class ChartOfAccountController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(AccountTypes? id) => View(id);
        public ActionResult Asset() => View("Home", AccountTypes.Asset);
        public ActionResult Liability() => View("Home", AccountTypes.Liability);
        public ActionResult Capital() => View("Home", AccountTypes.Capital);
        public ActionResult Expense() => View("Home", AccountTypes.Expense);
        public ActionResult Income() => View("Home", AccountTypes.Income);

        public ActionResult Redirect(Guid id)
        {
            var accountItem = Organization.ChartOfAccount.Find(id);
            return RedirectToAction("Home", "AccountItem", new { id = accountItem.Uid });
        }

        public PartialViewResult _ComboBox()
        {
            var accounts = Organization.ChartOfAccount.GetAccountsList();
            return PartialView(accounts);
        }

        public ActionResult New()
        {
            return View();
        }

        [ValidateInput(false)]
        public PartialViewResult _Tree(AccountTypes id)
        {
            ViewBag.id = id;
            var accountItems = Organization.ChartOfAccount.GetByType(id);
            return PartialView(accountItems);
        }



        public ActionResult UpdateBalance()
        {
            Organization.ChartOfAccount.UpdateBalance();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UpdateHistoryBalance()
        {
            Organization.ChartOfAccount.UpdateBalance();
            Organization.ChartOfAccount.GenerateHistoryBalance();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult ReAssignNumber()
        {
            Organization.ChartOfAccount.ReAssignNumber();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public PartialViewResult _FocusAccountsPreview(AccountTypes? accountType = null)
        {
            var accounts = Organization.ChartOfAccount
                .GetFocusAccount(accountType)
                .Where(a => a.CurrentBalance != 0)
                .ToList();
            ViewBag.AccountType = accountType?.ToString();

            return PartialView(accounts);
        }


    }
}