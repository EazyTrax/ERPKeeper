using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.Sale
{
    [Route("/API/{CompanyId}/Customers/Sales/{SaleId:Guid}/{controller}/{action=Index}")]
    public class _SaleBaseController : API_BaseController
    {

        public Guid SaleId => Guid.Parse(RouteData.Values["SaleId"].ToString());


    }
}
