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

namespace ERPKeeperCore.Web.Areas.Investors.Controllers
{
    [Area("Investors")]
    [Route("/{CompanyId}/Investors/{controller=Home}/{action=Index}/{id?}")]
    public class _Investors_Base_Controller : DefaultController
    {


    }
}