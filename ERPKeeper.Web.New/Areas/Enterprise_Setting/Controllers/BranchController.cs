
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Enterprise_Setting.Controllers
{
    [Route("/{CompanyId}/{Setting}/Branches/{BranchId}/{action=Index}/")]
    public class BranchController : _Enterprise_Setting_BaseController
    {
        public IActionResult Index(Guid BranchId)
        {
            var Branch = Organization.ErpCOREDBContext.Branches.Find(BranchId);

            if (Branch == null)
                return NotFound();


            return View(Branch);
        }

       

        [HttpPost]
        public IActionResult Update(Guid BranchId, Enterprise.Models.Info.Branch model)
        {
         
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
