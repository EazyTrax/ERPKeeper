using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
 
namespace ERPKeeper.Web.New.API.Assets.Asset
{
    [Route("/API/{CompanyId}/Assets/{AssetId:Guid}/{controller}/{action=Index}")]
    public class Asset_BaseController : APIBaseController
    {
        public Guid AssetId => Guid.Parse(HttpContext.GetRouteData().Values["AssetId"].ToString());

    }
}
