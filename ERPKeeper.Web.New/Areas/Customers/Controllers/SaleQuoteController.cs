﻿using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/Customers/SaleQuotes/{QuoteId:Guid}/{action=index}")]
    public class SaleQuoteController : _Customers_Base_Controller
    {

        public Guid QuoteId => Guid.Parse(RouteData.Values["QuoteId"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.SaleQuotes.Find(QuoteId);

            if (transcation == null)
                return NotFound();

            return View(transcation);
        }


        public IActionResult Items()
        {
            var transcation = Organization.SaleQuotes.Find(QuoteId);
            return View(transcation);
        }

        [HttpPost]
        public IActionResult Update(SaleQuote model)
        {
            var transcation = Organization.SaleQuotes.Find(QuoteId);

            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.No = model.No;

            transcation.UpdateBalance();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Export()
        {
            var transcation = Organization.SaleQuotes.Find(QuoteId);
            transcation.UpdateBalance();
            Organization.SaveChanges();

            return View(transcation);
        }


        public IActionResult Documents()
        {
            var transcation = Organization.SaleQuotes.Find(QuoteId);
            return View(transcation);
        }

    }
}
