
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Products.Controllers
{
    [Route("/{CompanyId}/{area}/Groups/{itemGroupUid:Guid}/{action=Index}")]
    public class GroupController : Base_ProductsController
    {
        public IActionResult Index(Guid itemGroupUid)
        {
            var i = Organization.GroupItems.Find(itemGroupUid);
            return View(i);
        }
    }
}
