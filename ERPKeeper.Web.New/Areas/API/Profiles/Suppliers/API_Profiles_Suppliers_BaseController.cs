using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers
{
    [Route("/API/{CompanyId}/Profiles/Suppliers/{controller}/{action=Index}")]
    public class API_Profiles_Suppliers_BaseController : API_BaseController
    {


    }
}
