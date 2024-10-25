using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/Customers/SaleQuotes/{Id:Guid}/{action=index}")]
    public class SaleQuoteController : _Customers_Base_Controller
    {

        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.SaleQuotes.Find(Id);

            if (transcation == null)
                return NotFound();

            return View(transcation);
        }


        public IActionResult Items()
        {
            var transcation = Organization.SaleQuotes.Find(Id);

            transcation.Reorder();
            Organization.SaveChanges();

            return View(transcation);
        }

        [Route("/{CompanyId}/Customers/SaleQuotes/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            return View(transcation);
        }

        [Route("/{CompanyId}/Customers/SaleQuotes/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);

            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Customers/SaleQuotes/{Id}/Items");
        }

        [HttpPost]
        public IActionResult Update(SaleQuote model)
        {
            var transcation = Organization.SaleQuotes.Find(Id);

            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.No = model.No;

            transcation.UpdateBalance();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Export()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.UpdateBalance();
            Organization.SaveChanges();

            return View(transcation);
        }


        public IActionResult Documents()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            return View(transcation);
        }

    }
}
