using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Taxes.Controllers
{
    [Route("/{CompanyId}/Taxes/TaxCodes/{TaxCodeId:Guid}/{action=Index}/{id?}")]
    public class TaxCodeController : Base_TaxesController
    {
        public IActionResult Index(Guid TaxCodeId)
        {
            var TaxCode = OrganizationCore.TaxCodes.Find(TaxCodeId);
            return View(TaxCode);
        }

      
        [HttpPost]
        public IActionResult Update(Guid TaxCodeId, ERPKeeperCore.Enterprise.Models.Taxes.TaxCode model)
        {
            var TaxCode = OrganizationCore.TaxCodes.Find(TaxCodeId);

            if (TaxCode != null)
            {
                TaxCode.Name = model.Name;
                //TaxCode.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////TaxCode.CreatedDate = model.CreatedDate;
                //TaxCode.DueDate = model.DueDate;
                //TaxCode.CloseDate = model.CloseDate;

                //TaxCode.ProjectUid = model.ProjectUid;
                //TaxCode.Detail = model.Detail;

                OrganizationCore.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
