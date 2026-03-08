using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Accountant.Controllers
{
    [Area("Accountant")]
    [Route("/Accountant/{Controller=Home}/{Action=Index}")]
    public class _AccountantBaseController : _AccountantRole_BaseController
    {
        public _AccountantBaseController()
        {



        }
    }
}