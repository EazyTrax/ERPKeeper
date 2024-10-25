using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Enterprise_Setting.Controllers
{
    [Area("Enterprise_Setting")]
    [Route("/{CompanyId}/{Setting}/{controller=Home}/{action=Index}/{id?}")]
    public class _Enterprise_Setting_BaseController : DefaultController
    {

      

    }
}
