
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{
    [Route("/{CompanyId}/Products/Items/{ItemUid:Guid}/{action=Index}")]
    public class ItemController : Base_ProductsController
    {
        public Guid ItemUid => Guid.Parse(RouteData.Values["ItemUid"].ToString());
        public Enterprise.Models.Items.Item Item => OrganizationCore.Items.Find(ItemUid);

        public IActionResult Index() => View(Item);
        public IActionResult Quotes() => View(Item);
        public IActionResult Purchases() => View(Item);
        public IActionResult Sales() => View(Item);

        [HttpPost]
        public IActionResult Update(ERPKeeperCore.Enterprise.Models.Items.Item model)
        {

            OrganizationCore.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

      


      

        public IActionResult UpdateStock()
        {
            
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
