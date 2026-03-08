using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ERPKeeperCore.Web.Areas.Accountant.Controllers
{
    [Area("Accountant")]
    [Authorize]
    [Route("/Accountant/{Controller=Home}/{Action=Index}")]
    public class _AccountantBaseController : ERPKeeperCore.Web.Controllers._BaseController
    {
    

        public _AccountantBaseController()
        {



        }
    }
}