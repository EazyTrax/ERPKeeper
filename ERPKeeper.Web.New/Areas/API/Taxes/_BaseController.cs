using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Taxes
{
    [Route("/API/{CompanyId}/Taxes/{controller}/{action=Index}")]
    public class IncomeTaxes_BaseController : APIBaseController
    {



    }
}
