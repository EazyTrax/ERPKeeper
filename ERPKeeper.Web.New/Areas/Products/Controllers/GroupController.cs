
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{
    [Route("/Products/Groups/{itemGroupUid:Guid}/{action=Index}")]
    public class GroupController : Base_ProductsController
    {
        public IActionResult Index(Guid itemGroupUid)
        {
            var i = Organization.ItemGroups.Find(itemGroupUid);
            return View(i);
        }
    }
}
