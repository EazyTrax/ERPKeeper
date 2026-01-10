using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Accounting
{
    [Route("/API/Accounting/{controller}/{action=Index}")]
    public class API_Accounting_BaseController : API_BaseController
    {


    }
}
