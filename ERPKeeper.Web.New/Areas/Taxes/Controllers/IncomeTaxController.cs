using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Taxes.Controllers
{
    [Route("/{CompanyId}/Taxes/IncomeTaxes/{IncomeTaxId:Guid}/{action=Index}/{id?}")]
    public class IncomeTaxController : Base_TaxesController
    {
        public IActionResult Index(Guid IncomeTaxId)
        {
            var IncomeTax = OrganizationCore.IncomeTaxes.Find(IncomeTaxId);
            return View(IncomeTax);
        }

      
        [HttpPost]
        public IActionResult Update(Guid IncomeTaxId, ERPKeeperCore.Enterprise.Models.Taxes.IncomeTax model)
        {
            var IncomeTax = OrganizationCore.IncomeTaxes.Find(IncomeTaxId);

            if (IncomeTax != null)
            {
                //IncomeTax.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////IncomeTax.CreatedDate = model.CreatedDate;
                //IncomeTax.DueDate = model.DueDate;
                //IncomeTax.CloseDate = model.CloseDate;

                //IncomeTax.ProjectUid = model.ProjectUid;
                //IncomeTax.Detail = model.Detail;

                OrganizationCore.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
