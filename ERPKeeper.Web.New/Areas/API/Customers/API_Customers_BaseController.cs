using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.New.API.Customers
{
    [Route("/API/{CompanyId}/Customers/{controller}/{action=Index}")]
    public class API_Customers_BaseController : API_BaseController
    {
 

    }
}
