
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{

    [Route("/{CompanyId}/{area}/{controller=Home}/{brandUid:Guid}/{action=Index}")]
    public class BrandController : Base_ProductsController
    {
        public IActionResult Index(Guid brandUid)
        {
            var i = EnterpriseRepo.Brands.Find(brandUid);
            return View(i);
        }
    }
}
