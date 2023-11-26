using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Equity
{
    [Route("/API/Equity/{controller}/{action=Index}")]
    public class BaseController : APIBaseController
    {
 

    }
}
