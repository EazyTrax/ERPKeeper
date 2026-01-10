using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.PurchaseQuote
{
    [Route("/API/Suppliers/PurchaseQuotes/{Id:Guid}/{controller}/{action=Index}")]
    public class API_Suppliers_PurchaseQuote_BaseController : API_BaseController
    {

        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());


    }
}
