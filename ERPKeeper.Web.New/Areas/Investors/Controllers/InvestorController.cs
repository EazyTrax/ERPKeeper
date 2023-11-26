using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Investors.Controllers
{
    [Route("/{CompanyId}/{area}/Owners/{OwnerUid:Guid}/{action=Index}")]
    public class OwnerController : Base_InvestorsController
    {

        public IActionResult Index(Guid OwnerUid)
        {
            var Owner = Organization.Investors.Find(OwnerUid);
            return View(Owner);
        }

        public IActionResult Payments(Guid OwnerUid)
        {
            var Owner = Organization.Investors.Find(OwnerUid);
            return View(Owner);
        }

    }
}
