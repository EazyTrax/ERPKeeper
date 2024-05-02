using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{

    [Route("/{CompanyId}/Products/{controller=Home}/{action=Index}")]
    public class RefreshController : Base_ProductsController
    {
        public IActionResult Stock()
        {

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
