using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Suppliers.Controllers
{
    [Route("/{CompanyId}/Profiles/Suppliers/PurchaseQuotes/{QuoteId:Guid}/{action=index}")]
    public class PurchaseQuoteController : _Profiles_Suppliers_Base_Controller
    {

        public Guid QuoteId => Guid.Parse(RouteData.Values["QuoteId"].ToString());

        public IActionResult Index()
        {
            var transcation = OrganizationCore.PurchaseQuotes.Find(QuoteId);
            return View(transcation);
        }


        public IActionResult Items()
        {
            var transcation = OrganizationCore.PurchaseQuotes.Find(QuoteId);
            return View(transcation);
        }

        public IActionResult Update(PurchaseQuote model)
        {
            var transcation = OrganizationCore.PurchaseQuotes.Find(QuoteId);

            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.No = model.No;

            transcation.UpdateBalance();
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Export()
        {
            var transcation = OrganizationCore.PurchaseQuotes.Find(QuoteId);
            transcation.UpdateBalance();
            OrganizationCore.SaveChanges();

            return View(transcation);
        }


        public IActionResult Documents()
        {
            var transcation = OrganizationCore.PurchaseQuotes.Find(QuoteId);
            return View(transcation);
        }

    }
}
