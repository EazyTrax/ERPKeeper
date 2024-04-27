using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.Controllers
{
    [Authorize]
    [Route("/{CompanyId}/{area=Dashboard}/{controller=Home}/{action=Index}/{id?}")]
    public class BaseNodeController : Controller
    {
        public Guid CurrentMakerId => Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public String CompanyId => HttpContext.GetRouteData().Values["CompanyId"].ToString();


        private ERPKeeperCore.Enterprise.DAL.EnterpriseRepo _Organization;
        public ERPKeeperCore.Enterprise.DAL.EnterpriseRepo EnterpriseRepo
        {
            get
            {
                if (_Organization == null)
                    _Organization = new ERPKeeperCore.Enterprise.DAL.EnterpriseRepo(CompanyId);
                return _Organization;
            }
        }


        public BaseNodeController()
        {

        }
    }
}

