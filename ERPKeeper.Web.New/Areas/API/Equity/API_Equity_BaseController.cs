using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Equity
{
    [Route("/API/Equity/{controller}/{action=Index}")]
    public class API_Equity_BaseController : API_BaseController
    {
 

    }
}
