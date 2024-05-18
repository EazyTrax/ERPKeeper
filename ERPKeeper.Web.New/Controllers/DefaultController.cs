using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ERPKeeperCore.Enterprise;

namespace ERPKeeperCore.Web.Controllers
{
    [Authorize]
    [Route("/{CompanyId}/{area=Dashboard}/{controller=Home}/{action=Index}/{id?}")]

    public class DefaultController : Controller
    {
        public Guid AuthorizeUserId => Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public String CompanyId => HttpContext.GetRouteData().Values["CompanyId"].ToString();


        private EnterpriseRepo _Organization;
        public EnterpriseRepo OrganizationCore
        {
            get
            {
                if (_Organization == null)
                    _Organization = new ERPKeeperCore.Enterprise.EnterpriseRepo(CompanyId,true);
                return _Organization;
            }
        }


        public DefaultController()
        {

        }
    }
}

