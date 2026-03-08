using ERPKeeperCore.Web.Areas.Accounting.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Assets.Controllers
{
    [Area("Assets")]
    public class AssetsArea_BaseController : _AccountantRole_BaseController
    {
       

    }
}