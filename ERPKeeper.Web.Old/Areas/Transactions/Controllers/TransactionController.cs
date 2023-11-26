
using ERPKeeper.Node.Models.Accounting.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Transactions.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class TransactionController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Redirect(Guid id, ERPObjectType type)
        {
            string areaName = string.Empty;
            switch (type)
            {
                case ERPObjectType.Sale:
                    areaName = "Customers";
                    break;
                case ERPObjectType.Purchase:
                    areaName = "Suppliers";
                    break;
                case ERPObjectType.FundTransfer:
                case ERPObjectType.EarnestPayment:
                case ERPObjectType.EarnestReceive:
                    areaName = "Financial";
                    break;

                case ERPObjectType.EmployeePayment:
                    return RedirectToAction("Home", "Payment", new { id = id, Area = "Employees" });

                case ERPObjectType.TaxPeriod:
                    return RedirectToAction("Home", "TaxPeriod", new { id = id, Area = "Taxes" });

                case ERPObjectType.ReceivePayment:
                    return RedirectToAction("Home", "ReceivePayment", new { id = id, Area = "Customers" });


                case ERPObjectType.SupplierPayment:
                    return RedirectToAction("Home", "SupplierPayment", new { id = id, Area = "Suppliers" });


                case ERPObjectType.LiabilityPayment:
                    return RedirectToAction("Home", "LiabilityPayment", new { id = id, Area = "Financial" });


                case ERPObjectType.JournalEntry:
                    return RedirectToAction("Home", "JournalEntry", new { id = id, Area = "Accounting" });


                case ERPObjectType.FixedAsset:
                    return RedirectToAction("Home", "FixedAsset", new { id = id, Area = "Assets" });


                case ERPObjectType.DeprecateSchedule:
                    var deprecatedAssetGuid = Organization.FixedAssetDreprecateSchedules.Find(id)?.FixedAssetUid;
                    return RedirectToAction("depreciationschedules", "FixedAsset", new { id = deprecatedAssetGuid, Area = "Assets" });


                case ERPObjectType.CapitalActivity:
                    return RedirectToAction("Home", "CapitalInvestmet", new { id = id, Area = "Investors" });



                case ERPObjectType.OpeningEntry:
                    return RedirectToAction("Home", "accountitem", new { id = id, Area = "accounting" });

                default:
                    break;
            }


            return RedirectToAction("Home", type.ToString(), new { id = id, Area = areaName });

        }

    }
}