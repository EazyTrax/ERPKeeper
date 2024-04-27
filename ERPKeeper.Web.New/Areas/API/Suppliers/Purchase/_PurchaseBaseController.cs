using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Suppliers.Purchases.Purchase
{
    [Route("/API/{CompanyId}/Suppliers/Purchases/{PurchaseId:Guid}/{controller}/{action=Index}")]
    public class _PurchaseBaseController : API_BaseController
    {
 
        public Guid PurchaseId => Guid.Parse(RouteData.Values["PurchaseId"].ToString());


    }
}
