using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Products.Controllers
{

    [Route("/{CompanyId}/{area}/{controller=Home}/{action=Index}")]
    public class RefreshController : Base_ProductsController
    {
        public IActionResult Stock()
        {
            Organization.InventoryItemsDal.UpdateStockAmount();
            Organization.InventoryItemsDal.UpdateEstimateAmount();


            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
