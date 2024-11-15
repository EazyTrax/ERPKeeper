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
    [Route("/{CompanyId}/Logistic/{controller=Home}/{action=Index}")]
    public class _Logistic_BaseController : DefaultController
    {

      

    }
}
