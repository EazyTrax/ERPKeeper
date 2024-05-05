using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Employees
{
    [Route("/API/{CompanyId}/Profiles/Employees/{controller}/{action=Index}")]
    public class API_Profiles_Employees_BaseController : API_BaseController
    {


    }
}

