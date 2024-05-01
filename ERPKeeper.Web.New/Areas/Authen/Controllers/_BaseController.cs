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
using ERPKeeperCore.Enterprise;

namespace ERPKeeperCore.Web.Areas.Authen.Controllers
{
    [Area("Authen")]
    [Route("/{area}/{controller=SignIn}/{action=Index}")]
    [AllowAnonymous]
    public class BaseController : Controller
    {
        protected EnterpriseRepo organizationRepo;

    }
}