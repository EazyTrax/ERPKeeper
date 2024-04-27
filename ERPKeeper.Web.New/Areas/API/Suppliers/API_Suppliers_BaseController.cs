using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Suppliers
{
    [Route("/API/{CompanyId}/Suppliers/{controller}/{action=Index}")]
    public class API_Suppliers_BaseController : API_BaseController
    {
 

    }
}
