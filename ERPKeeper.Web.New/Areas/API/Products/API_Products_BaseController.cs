using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Products
{
    [Route("/API/{CompanyId}/Products/{controller}/{action=Index}")]
    public class API_Products_BaseController : API_BaseController
    {



    }
}
