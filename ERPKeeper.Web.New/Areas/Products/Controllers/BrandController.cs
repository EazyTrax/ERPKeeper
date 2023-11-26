
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Products.Controllers
{

    [Route("/{CompanyId}/{area}/{controller=Home}/{brandUid:Guid}/{action=Index}")]
    public class BrandController : Base_ProductsController
    {
        public IActionResult Index(Guid brandUid)
        {
            var i = Organization.ItemBrands.Find(brandUid);
            return View(i);
        }
    }
}
