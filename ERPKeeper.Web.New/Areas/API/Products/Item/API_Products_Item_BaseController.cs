using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.New.API.Products.Item
{
    [Route("/API/Products/Items/{ItemId:guid}/{controller}/{action=Index}")]
    public class API_Products_Item_BaseController : API_BaseController
    {
        public Guid ItemId => Guid.Parse(HttpContext.GetRouteData().Values["ItemId"].ToString());


    }
    
}
