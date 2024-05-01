using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Routing;
using ERPKeeperCore.Enterprise;

namespace ERPKeeperCore.Web.API
{
    [Area("API")]
    [Route("/API/{CompanyId}/{controller=Home}/{action=Index}/{id?}")]
    public class API_BaseController : Controller
    {
        public Guid CurrentMakerId => Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public String CompanyId => HttpContext.GetRouteData().Values["CompanyId"].ToString();

        private EnterpriseRepo _Organization;
        public EnterpriseRepo Organization
        {
            get
            {
                if (_Organization == null)
                    _Organization = new Enterprise.EnterpriseRepo(CompanyId, false);

                return _Organization;
            }
        }


        public API_BaseController()
        {

        }
    }
}