﻿using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Taxes.Controllers
{
    [Route("/{CompanyId}/Taxes/TaxPeriods/{TaxPeriodId:Guid}/{action=Index}/{id?}")]
    public class TaxPeriodController : Base_TaxesController
    {
        public IActionResult Index(Guid TaxPeriodId)
        {
            var TaxPeriod = Organization.TaxPeriods.Find(TaxPeriodId);
            TaxPeriod.Sales.Where(s => s.TaxCode == null).ToList()
                .ForEach(s => s.TaxPeriod = null);

            TaxPeriod.Purchases.Where(s => s.TaxCode == null).ToList()
                .ForEach(s => s.TaxPeriod = null);
            Organization.SaveChanges();

            TaxPeriod.UpdateBalance();
            Organization.SaveChanges();




            return View(TaxPeriod);
        }

        public IActionResult AutoAssign(Guid TaxPeriodId, bool IsHistoryAssign = false)
        {
            var TaxPeriod = Organization.TaxPeriods.Find(TaxPeriodId);

            var assignAbleSales = Organization.ErpCOREDBContext.Sales
                .Where(x => x.TaxPeriodId == null && x.TaxCode != null)
                .Where(x => (IsHistoryAssign || x.Date >= TaxPeriod.StartDate) && x.Date <= TaxPeriod.EndDate)
                .Where(x => x.TaxCode.AbleToAssignToTaxPeriod)
                .ToList();

            Console.WriteLine("assignAbleSales.Count: " + assignAbleSales.Count);
            assignAbleSales.ForEach(x => x.TaxPeriodId = TaxPeriodId);

            var assignAblePurchases = Organization.ErpCOREDBContext.Purchases
                .Where(x => x.TaxPeriodId == null && x.TaxCode != null)
                .Where(x => (IsHistoryAssign || x.Date >= TaxPeriod.StartDate) && x.Date <= TaxPeriod.EndDate)
               .Where(x => x.TaxCode.AbleToAssignToTaxPeriod)
               .ToList();

            Console.WriteLine("assignAblePurchases.Count: " + assignAblePurchases.Count);
            assignAblePurchases.ForEach(x => x.TaxPeriodId = TaxPeriodId);

            TaxPeriod.UpdateBalance();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Export(Guid TaxPeriodId)
        {
            var TaxPeriod = Organization.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }
        public IActionResult Sales(Guid TaxPeriodId)
        {
            var TaxPeriod = Organization.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }
        public IActionResult Purchases(Guid TaxPeriodId)
        {
            var TaxPeriod = Organization.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }

        [HttpPost]
        public IActionResult Update(Guid TaxPeriodId, ERPKeeperCore.Enterprise.Models.Taxes.TaxPeriod model)
        {
            var TaxPeriod = Organization.TaxPeriods.Find(TaxPeriodId);

            if (TaxPeriod != null)
            {
                //TaxPeriod.Title = model.Title;
                //TaxPeriod.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////TaxPeriod.CreatedDate = model.CreatedDate;
                //TaxPeriod.DueDate = model.DueDate;
                //TaxPeriod.CloseDate = model.CloseDate;

                //TaxPeriod.ProjectUid = model.ProjectUid;
                //TaxPeriod.Detail = model.Detail;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Delete(Guid TaxPeriodId)
        {
            var TaxPeriod = Organization.TaxPeriods.Find(TaxPeriodId);

            if (TaxPeriod.IsPosted == false && TaxPeriod.Sales.Count == 0 && TaxPeriod.Purchases.Count == 0)
            {
                Organization.ErpCOREDBContext.TaxPeriods.Remove(TaxPeriod);
                Organization.SaveChanges();
            }
            return Redirect($"/{CompanyId}/Taxes/TaxPeriods");
        }
    }
}
