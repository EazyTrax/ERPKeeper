using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Logistic.Controllers
{
    [Area("Logistic")]
    [Route("/Logistic/{controller=Home}/{action=Index}")]
    public class _Logistic_BaseController : _BaseController
    {

      

    }
}
