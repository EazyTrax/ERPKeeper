using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.SupplierPayment
{
    [Route("/API/{CompanyId}/Suppliers/SupplierPayments/{Id:Guid}/{controller}/{action=Index}")]
    public class _API_Suppliers_SupplierPayment_BaseController : API_BaseController
    {

        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());


    }
}
