using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Assets
{
    [Route("/API/{CompanyId}/Assets/{controller}/{action=Index}")]
    public class Assets_BaseController : APIBaseController
    {



    }
}
