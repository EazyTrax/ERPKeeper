using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ERPKeeperCore.Enterprise;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.DBContext;

namespace ERPKeeperCore.Web.Controllers
{
    [Authorize]
    [Authorize(Roles = "Accountant,Administrator")]
    public class _AccountantRole_BaseController : _BaseController
    {
       
    }
}

