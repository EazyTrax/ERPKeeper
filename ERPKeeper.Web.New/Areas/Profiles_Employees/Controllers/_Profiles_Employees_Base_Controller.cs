using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles_Employees.Controllers
{
    [Area("Profiles_Employees")]
    [Route("/{CompanyId}/Profiles/Employees/{controller=Home}/{action=Index}/{id?}")]
    public class _Profiles_Employees_Base_Controller : BaseNodeController
    {


    }
}