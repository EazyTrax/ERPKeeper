using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Operation
{
    [Route("/API/Operations/{controller}/{action=Index}")]
    public class BaseController : APIBaseController
    {
 

    }
}
