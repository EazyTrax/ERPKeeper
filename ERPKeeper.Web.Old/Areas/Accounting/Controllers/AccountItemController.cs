using ERPKeeper.Node.Models.Accounting;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    [RouteArea("Accounting")]
    [RoutePrefix("AccountItem")]
    [Route("{id}/{action=Home}")]
    public class AccountItemController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult RemoveFromPreview(Guid id)
        {
            Organization.PreviewAccounts.Remove(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult AddToMyFavoriteAccount(Guid id)
        {
            var myFavoriteAccount = Organization.PreviewAccounts.Find(id, CurrentUser.ProfileUid);
            if (myFavoriteAccount == null)
                Organization.PreviewAccounts.Create(id, CurrentUser.ProfileUid);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public PartialViewResult _ShowFavoriteAccountDetail(Guid id)
        {
            var myFavoriteAccount = Organization.PreviewAccounts.Find(id, CurrentUser.ProfileUid);
            return PartialView(myFavoriteAccount);
        }

        public ActionResult NewAccount()
        {
            return View();
        }



        public ActionResult New(AccountTypes accountType, AccountSubTypes accountSubType = AccountSubTypes.None)
        {
            var accountItem = Organization.ChartOfAccount.newItem(accountType, accountSubType);
            return View("Home", accountItem);
        }

        public ActionResult Home(Guid id)
        {
            var accountItem = Organization.ChartOfAccount.Find(id);
            ViewBag.AccountSubTypes = Organization.ChartOfAccount.GetSubType(accountItem.Type);
            ViewBag.AccountReportGroups = Organization.ChartOfAccount.GetAccountReportGroups(accountItem.Type);

            return View("Home", accountItem);
        }


        public ActionResult Ledger(Guid id, DateTime? date)
        {
            ViewBag.TrnDate = date;
            var accountItem = Organization.ChartOfAccount.Find(id);
            ViewBag.AccountSubTypes = Organization.ChartOfAccount.GetSubType(accountItem.Type);

            return View(accountItem);
        }


        public ActionResult History(Guid id)
        {
            var accountItem = Organization.ChartOfAccount.Find(id);
            return View(accountItem);
        }


        [HttpPost]
        public ActionResult Save(AccountItem accountItem)
        {
            accountItem = Organization.ChartOfAccount.Update(accountItem);
            return RedirectToAction("Home", new { id = accountItem.Uid });
        }


        [Authorize.ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Admin)]
        public ActionResult Delete(Guid id)
        {
            var accountItem = Organization.ChartOfAccount.Find(id);
            Organization.ChartOfAccount.Remove(accountItem);

            return RedirectToAction("Home", "Accounting");
        }





        public ActionResult Chart(Guid id)
        {
            var accountItem = Organization.ChartOfAccount.Find(id);
            return View(accountItem);
        }


        public PartialViewResult _MiniChart(Guid? id)
        {
            var accountItem = Organization.ChartOfAccount.Find(id);
            if (accountItem != null)
            {
                ViewBag.AccountItem = accountItem;
                var historyBalances = Organization.ChartOfAccount.GetHistoryBalance(accountItem);
                return PartialView("_MiniChart", historyBalances);
            }
            return null;
        }

        public PartialViewResult _Chart(Guid id)
        {
            ViewBag.id = id;
            var accountItem = Organization.ChartOfAccount.Find(id);
            var historyBalances = Organization.ChartOfAccount.GetHistoryBalance(accountItem);


            return PartialView("_Chart", historyBalances);
        }

        public ActionResult Ledgers(Guid id, DateTime? Date)
        {
            ViewBag.id = id;
            ViewBag.TrnDate = Date;

            var accountItem = Organization.ChartOfAccount.Find(id);
            return View(accountItem);
        }



        public PartialViewResult PartialGridViewHistory(Guid id)
        {
            ViewBag.id = id;
            var _Account = Organization.ChartOfAccount.Find(id);
            var historyBalance = Organization.ChartOfAccount.GetHistoryBalance(_Account);

            return PartialView(historyBalance);
        }

        public PartialViewResult PartialGridViewLedger(Guid id, DateTime? date)
        {
            ViewBag.id = id;
            ViewBag.TrnDate = date;

            var ledgers = Organization.LedgersDal.GetLedgers(id, date);
            return PartialView(ledgers);
        }



        public ActionResult UpdateHistoryBalance(Guid id)
        {
            var _Account = Organization.ChartOfAccount.Find(id);
            Organization.ChartOfAccount.GenerateHistoryBalance(_Account);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult UpdateBalance(Guid id)
        {
            var accountItem = Organization.ChartOfAccount.Find(id);
            Organization.ChartOfAccount.UpdateBalance(accountItem);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}