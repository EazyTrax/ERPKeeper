using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ERPKeeperCore.Web.Areas.My.Controllers
{
    [Area("My")]
    [Authorize]
    [Route("/My/{Controller=Home}/{Action=Index}")]
    public class _MyBaseController : ERPKeeperCore.Web.Controllers._BaseController
    {
        public _MyBaseController()
        {

        }
    }
}