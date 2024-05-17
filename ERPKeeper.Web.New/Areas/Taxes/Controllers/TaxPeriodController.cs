using ERPKeeperCore.Web.Controllers;
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
            var TaxPeriod = OrganizationCore.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }
        public IActionResult Export(Guid TaxPeriodId)
        {
            var TaxPeriod = OrganizationCore.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }
        public IActionResult Sales(Guid TaxPeriodId)
        {
            var TaxPeriod = OrganizationCore.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }
        public IActionResult Purchases(Guid TaxPeriodId)
        {
            var TaxPeriod = OrganizationCore.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }

        [HttpPost]
        public IActionResult Update(Guid TaxPeriodId, ERPKeeperCore.Enterprise.Models.Taxes.TaxPeriod model)
        {
            var TaxPeriod = OrganizationCore.TaxPeriods.Find(TaxPeriodId);

            if (TaxPeriod != null)
            {
                //TaxPeriod.Title = model.Title;
                //TaxPeriod.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////TaxPeriod.CreatedDate = model.CreatedDate;
                //TaxPeriod.DueDate = model.DueDate;
                //TaxPeriod.CloseDate = model.CloseDate;

                //TaxPeriod.ProjectUid = model.ProjectUid;
                //TaxPeriod.Detail = model.Detail;

                OrganizationCore.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
