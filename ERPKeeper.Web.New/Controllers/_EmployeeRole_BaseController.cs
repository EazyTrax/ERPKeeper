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
    [Authorize(Roles = "Employee")]
    public class _EmployeeRole_BaseController : _BaseController
    {

    }
}

