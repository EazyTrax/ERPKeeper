using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Taxes.Controllers
{
    [Route("/{CompanyId}/{area}/TaxPeriods/{TaxPeriodId:Guid}/{action=Index}/{id?}")]
    public class TaxPeriodController : Base_TaxesController
    {
        public IActionResult Index(Guid TaxPeriodId)
        {
            var TaxPeriod = EnterpriseRepo.TaxPeriods.Find(TaxPeriodId);
            return View(TaxPeriod);
        }

     
        [HttpPost]
        public IActionResult Update(Guid TaxPeriodId, ERPKeeperCore.Enterprise.Models.Taxes.TaxPeriod model)
        {
            var TaxPeriod = EnterpriseRepo.TaxPeriods.Find(TaxPeriodId);

            if (TaxPeriod != null)
            {
                //TaxPeriod.Title = model.Title;
                //TaxPeriod.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////TaxPeriod.CreatedDate = model.CreatedDate;
                //TaxPeriod.DueDate = model.DueDate;
                //TaxPeriod.CloseDate = model.CloseDate;

                //TaxPeriod.ProjectUid = model.ProjectUid;
                //TaxPeriod.Detail = model.Detail;

                EnterpriseRepo.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
