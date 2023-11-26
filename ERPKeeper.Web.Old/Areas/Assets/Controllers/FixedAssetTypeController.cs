using ERPKeeper.Node.Models.Assets;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Assets.Controllers
{
    public class FixedAssetTypeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.ExpenseAccount = Organization.ChartOfAccount.ExpenseAccounts;
            ViewBag.AssetAccount = Organization.ChartOfAccount.AssetAccounts;
        }


        public ActionResult Home(Guid id)
        {
            var depreciationBasis = Organization.FixedAssetTypes.Find(id);
            return View(depreciationBasis);
        }

        public ActionResult New()
        {
            return View("Home");
        }

        public ActionResult Save(FixedAssetType model)
        {
            var exFixedAssetType = Organization.FixedAssetTypes.Find(model.Uid);

            if (exFixedAssetType == null)
                Organization.FixedAssetTypes.Add(model);
            else
                exFixedAssetType.Update(model);

            Organization.SaveChanges();
            return RedirectToAction("Home", new { id = model.Uid });
        }

        public ActionResult AutoCreateAccount(Guid id)
        {
            var FxAsstype = Organization.FixedAssetTypes.Find(id);

            if (FxAsstype.AwaitDeprecateAccount == null)
                FxAsstype.AwaitDeprecateAccount = Organization.ChartOfAccount.newItem(AccountTypes.Asset, AccountSubTypes.AwaitingDepreciation, "AWIT-FIX", "AWIT-FIX-" + FxAsstype.Name, "AWIT-FIX-" + FxAsstype.Name);

            if (FxAsstype.PurchaseAccUid == null)
                FxAsstype.AssetAccount = Organization.ChartOfAccount.newItem(AccountTypes.Asset, AccountSubTypes.FixedAsset, "FIX", "FIXASSET-" + FxAsstype.Name, "FIXASSET-" + FxAsstype.Name);

            if (FxAsstype.AmortizeExpenseAccUid == null)
                FxAsstype.AmortizeExpenseAccount = Organization.ChartOfAccount.newItem(AccountTypes.Expense, AccountSubTypes.AmortizeExpense, "EX-AMOR", "AMOR-" + FxAsstype.Name, "FIXASSET-" + FxAsstype.Name);

            if (FxAsstype.AccumulateDeprecateAccUid == null)
                FxAsstype.AccumulateDeprecateAcc = Organization.ChartOfAccount.newItem(AccountTypes.Asset, AccountSubTypes.AccDepreciation, "ACC", "ACCUMDEP-" + FxAsstype.Name, "FIXASSET-" + FxAsstype.Name);

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult ListFixedAssets(Guid id)
        {
            var depreciationBasis = Organization.FixedAssetTypes.Find(id);
            return View(depreciationBasis);
        }

        [ValidateInput(false)]
        public PartialViewResult PartialGridViewFixedAssets(Guid id)
        {
            ViewBag.id = id;

            var depreciationBasis = Organization.FixedAssetTypes.Find(id);
            var deprecatedAssets = depreciationBasis.FixedAssets.ToList();
            return PartialView(deprecatedAssets);
        }
    }
}