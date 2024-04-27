using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.New.API.Profiles
{
    [Route("/API/{CompanyId}/{controller}/{action=Index}")]
    public class API_Profiles_BaseController : API_BaseController
    {



    }
}
