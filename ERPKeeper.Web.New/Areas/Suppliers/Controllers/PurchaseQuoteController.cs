using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{
    [Route("/{CompanyId}/Suppliers/PurchaseQuotes/{Id:Guid}/{action=index}")]
    public class PurchaseQuoteController : _Suppliers_Base_Controller
    {

        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            return View(transcation);
        }


        public IActionResult Items()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
           
            transcation.UpdateItems();

            Organization.SaveChanges();
            return View(transcation);
        }

        [Route("/{CompanyId}/Suppliers/PurchaseQuotes/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            return View(transcation);
        }
        [Route("/{CompanyId}/Suppliers/PurchaseQuotes/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);
            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Suppliers/PurchaseQuotes/{Id}/Items");
        }

        public IActionResult Update(PurchaseQuote model)
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);

            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.No = model.No;

            transcation.UpdateBalance();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Export()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            transcation.UpdateBalance();
            Organization.SaveChanges();

            return View(transcation);
        }

        public IActionResult Delete()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);

            Organization.PurchaseQuotes.Delete(transcation);
            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Suppliers/Suppliers");
        }
        public IActionResult Documents()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            return View(transcation);
        }

    }
}
