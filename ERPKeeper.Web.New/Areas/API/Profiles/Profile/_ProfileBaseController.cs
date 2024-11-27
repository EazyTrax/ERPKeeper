using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Profiles.Profile
{
    [Route("/api/{CompanyId}/Profiles/{Id:Guid}/{controller}/{action=Index}")]
    public class _ProfileBaseController : API_BaseController
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());
    }
}
