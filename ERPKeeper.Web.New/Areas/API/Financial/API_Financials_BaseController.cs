using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.New.API.Financials
{
    [Route("/API/{CompanyId}/Financial/{controller}/{action=Index}")]
    public class API_Financials_BaseController : API_BaseController
    {
 

    }
}
