using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Storage.Controllers
{
    [Area("Storage")]
    [Route("/{area}/{controller=Home}/{action=Index}/{id?}")]
    public class _Storage_BaseController : _BaseController
    {



    }
}
