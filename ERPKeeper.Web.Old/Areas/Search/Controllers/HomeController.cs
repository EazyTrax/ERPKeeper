using ERPKeeper.Node.Models.Accounting.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Search.Controllers
{
    public class HomeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home => View();
        public PartialViewResult _SearchBox() => PartialView();

        public ActionResult Perform(string SearchString)
        {
            ViewBag.SearchString = SearchString;

            var searchModel = new Node.Models.Search.SearchModel(SearchString);

            if (SearchString == null)
            {
                return RedirectToAction("Home", "History", new { Area = "Search" });
            }
            else if (searchModel.ObjectType != null && searchModel.GroupString == null)
            {
                if (searchModel.ObjectType == ERPObjectType.Sale)
                    return RedirectToAction("Home", "Sales", new { Area = "Customers" });
                else if (searchModel.ObjectType == ERPObjectType.ReceivePayment)
                    return RedirectToAction("Home", "ReceivePayments", new { Area = "Customers" });
                else if (searchModel.ObjectType == ERPObjectType.Purchase)
                    return RedirectToAction("Home", "Purchases", new { Area = "Suppliers" });
                else if (searchModel.ObjectType == ERPObjectType.SupplierPayment)
                    return RedirectToAction("Home", "SupplierPayments", new { Area = "Suppliers" });
                else if (searchModel.ObjectType == ERPObjectType.Customer)
                    return RedirectToAction("Home", "Customers", new { Area = "Customers" });
                else if (searchModel.ObjectType == ERPObjectType.Supplier)
                    return RedirectToAction("Home", "Suppliers", new { Area = "Suppliers" });
                else if (searchModel.ObjectType == ERPObjectType.Employee)
                    return RedirectToAction("Home", "Employees", new { Area = "Employees" });

            }
            else if (searchModel.ObjectType != null && searchModel.GroupString != null && searchModel.NumberString == null)
            {
                if (searchModel.ObjectType == ERPObjectType.Customer)
                {
                    var customer = Organization.Customers.Search(searchModel.GroupString);
                    return RedirectToAction("Home", "Customer", new { Area = "Customers", id = customer.ProfileUid });
                }
                else if (searchModel.ObjectType == ERPObjectType.Supplier)
                {
                    var supplier = Organization.Suppliers.Search(searchModel.GroupString);
                    if (supplier != null)
                        return RedirectToAction("Home", "Supplier", new { Area = "Suppliers", id = supplier.ProfileUid });
                    else
                        return Redirect("/");
                }
                else if (searchModel.ObjectType == ERPObjectType.Employee)
                {
                    var customer = Organization.Employees.Search(searchModel.GroupString);
                    if (customer != null)
                        return RedirectToAction("Home", "Employee", new { Area = "Employees", id = customer.ProfileUid });
                    else
                        return Redirect("/");
                }

                else if (searchModel.ObjectType == ERPObjectType.Item)
                {
                    var item = Organization.Items.Search(searchModel.GroupString);
                    return RedirectToAction("Home", "Item", new { Area = "Items", id = item.Uid });
                }

            }
            else if (searchModel.ObjectType != null && searchModel.GroupString != null && searchModel.NumberString != null)
            {
                if (searchModel.ObjectType == ERPObjectType.Sale)
                {
                    var sale = Organization.Sales.Search(searchModel.GroupString, searchModel.NumberString);
                    return RedirectToAction("Home", "Sale", new { Area = "Customers", id = sale.Uid });
                }

            }

            return View(searchModel);
        }
    }
}