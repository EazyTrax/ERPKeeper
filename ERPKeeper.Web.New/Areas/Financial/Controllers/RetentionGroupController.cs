using ERPKeeperCore.Web.Areas.Financial.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Financials.Controllers
{

    [Route("/{CompanyId}/Financial/RetentionGroups/{Id:Guid}/{action=index}")]
    public class RetentionGroupController : Financial_BaseController
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.RetentionGroups.Find(Id);
            transcation.UpdateBalance();
            Organization.SaveChanges();

            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = Organization.RetentionGroups.Find(Id);
            transcation.UpdateBalance();
            Organization.SaveChanges();

            if (transcation.RetentionType.Direction == Enterprise.Models.Financial.Enums.RetentionDirection.Receive)
            {
                return View("Export_Receive", transcation);
            }
            else
            {
                return View("Export_Payment", transcation);
            }
        }
    }
}