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

namespace ERPKeeperCore.Web.New.API.Financials.RetentionGroup
{
    [Route("/API/{CompanyId}/Financial/RetentionGroups/{Id:guid}/{controller}/{action=Index}")]
    public class API_Financials_RetentionGroup_BaseController : API_BaseController
    {
        public Guid RetentionGroupId => Guid.Parse(HttpContext.GetRouteData().Values["Id"].ToString());


    }
}
