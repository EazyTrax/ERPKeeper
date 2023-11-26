using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
 
namespace ERPKeeper.Web.New.API.Assets.AssetType
{
    [Route("/API/{CompanyId}/AssetTypes/{AssetTypeId:Guid}/{controller}/{action=Index}")]
    public class AssetType_BaseController : APIBaseController
    {
        public Guid AssetTypeId => Guid.Parse(HttpContext.GetRouteData().Values["AssetTypeId"].ToString());

    }
}
