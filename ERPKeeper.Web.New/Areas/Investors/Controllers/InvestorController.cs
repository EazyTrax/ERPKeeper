using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Investors.Controllers
{
    [Route("/{CompanyId}/Investors/Investors/{OwnerUid:Guid}/{action=Index}")]
    public class OwnerController : _Investors_Base_Controller
    {

        public IActionResult Index(Guid OwnerUid)
        {
            var Owner = OrganizationCore.Investors.Find(OwnerUid);
            return View(Owner);
        }

        public IActionResult Payments(Guid OwnerUid)
        {
            var Owner = OrganizationCore.Investors.Find(OwnerUid);
            return View(Owner);
        }

    }
}
