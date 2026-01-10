using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
 
namespace ERPKeeperCore.Web.API.Assets.AssetType
{
    [Route("/API/AssetTypes/{AssetTypeId:Guid}/{controller}/{action=Index}")]
    public class AssetType_BaseController : API_BaseController
    {
        public Guid AssetTypeId => Guid.Parse(HttpContext.GetRouteData().Values["AssetTypeId"].ToString());

    }
}
