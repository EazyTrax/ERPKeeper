using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{

    [Route("/{CompanyId}/{area}/{controller=Home}/{action=Index}")]
    public class RefreshController : Base_ProductsController
    {
        public IActionResult Stock()
        {
            EnterpriseRepo.InventoryItemsDal.UpdateStockAmount();
            EnterpriseRepo.InventoryItemsDal.UpdateEstimateAmount();


            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
