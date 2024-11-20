using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.SaleQuote
{
    [Route("/API/{CompanyId}/Customers/SaleQuotes/{SaleQuoteId:Guid}/{controller}/{action=Index}")]
    public class API_Profiles_Customers_SaleQuote_BaseController : API_BaseController
    {

        public Guid SaleQuoteId => Guid.Parse(RouteData.Values["SaleQuoteId"].ToString());


    }
}
