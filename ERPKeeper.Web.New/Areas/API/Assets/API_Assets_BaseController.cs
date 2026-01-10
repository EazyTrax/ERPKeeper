using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Assets
{
    [Route("/API/Assets/{controller}/{action=Index}")]
    public class API_Assets_BaseController : API_BaseController
    {



    }
}
