using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles_Investors.Controllers
{
    [Route("/{CompanyId}/{area}/Owners/{OwnerUid:Guid}/{action=Index}")]
    public class OwnerController : _Profiles_Investors_Base_Controller
    {

        public IActionResult Index(Guid OwnerUid)
        {
            var Owner = EnterpriseRepo.Investors.Find(OwnerUid);
            return View(Owner);
        }

        public IActionResult Payments(Guid OwnerUid)
        {
            var Owner = EnterpriseRepo.Investors.Find(OwnerUid);
            return View(Owner);
        }

    }
}
