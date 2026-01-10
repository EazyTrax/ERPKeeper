using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Taxes.Controllers
{
    [Route("/Taxes/IncomeTaxes/{Id:Guid}/{action=Index}")]
    public class IncomeTaxController : Base_TaxesController
    {
        public IActionResult Index(Guid Id)
        {
            var IncomeTax = Organization.IncomeTaxes.Find(Id);
            return View(IncomeTax);
        }

      
        [HttpPost]
        public IActionResult Update(Guid IncomeTaxId, ERPKeeperCore.Enterprise.Models.Taxes.IncomeTax model)
        {
            var IncomeTax = Organization.IncomeTaxes.Find(IncomeTaxId);

            if (IncomeTax != null)
            {
                //IncomeTax.ResponsibleMemberUid = model.ResponsibleMemberUid;
                ////IncomeTax.CreatedDate = model.CreatedDate;
                //IncomeTax.DueDate = model.DueDate;
                //IncomeTax.CloseDate = model.CloseDate;

                //IncomeTax.ProjectUid = model.ProjectUid;
                //IncomeTax.Detail = model.Detail;

                Organization.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult UnPost(Guid Id)
        {
            var model = Organization.ErpCOREDBContext.IncomeTaxes.Find(Id);
            model.IsPosted = false;
            model.Transaction.ClearLedger();
            Organization.ErpCOREDBContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Issue(Guid Id)
        {
            var model = Organization.ErpCOREDBContext.IncomeTaxes.Find(Id);


            model.Status = Enterprise.Models.Taxes.Enums.IncomeTaxStatus.Issued;
            Organization.ErpCOREDBContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
