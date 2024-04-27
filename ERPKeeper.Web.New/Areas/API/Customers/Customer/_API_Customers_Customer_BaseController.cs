using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.New.API.Customers.Customers.Customer
{
    [Route("/API/{CompanyId}/Customers/Customers/{ProfileId:Guid}/{controller}/{action=Index}")]
    public class _API_Customers_Customer_BaseController : API_BaseController
    {
        public Guid ProfileId => Guid.Parse(RouteData.Values["ProfileId"].ToString());


    }
}
