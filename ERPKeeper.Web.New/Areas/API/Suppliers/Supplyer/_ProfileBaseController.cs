using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Suppliers.Suppliers.Supplyer
{
    [Route("/API/{CompanyId}/Suppliers/Suppliers/{ProfileId:Guid}/{controller}/{action=Index}")]
    public class BaseController : APIBaseController
    {
        public Guid ProfileId => Guid.Parse(RouteData.Values["ProfileId"].ToString());


    }
}
