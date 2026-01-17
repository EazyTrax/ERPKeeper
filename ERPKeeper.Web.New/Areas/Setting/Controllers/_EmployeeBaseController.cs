using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ERPKeeperCore.Web.Areas.Setting.Controllers
{
    [Area("Setting")]
    [Authorize]
    [Route("/Setting/{Controller=Home}/{Action=Index}")]
    public class _SettingBaseController : ERPKeeperCore.Web.Controllers._BaseController
    {
    

        public _SettingBaseController()
        {



        }
    }
}