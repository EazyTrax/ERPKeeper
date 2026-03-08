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
using ERPKeeperCore.Enterprise;

namespace ERPKeeperCore.Web.Areas.Authen.Controllers
{
    [Area("Authen")]
    [Route("/{area}/{controller=SignIn}/{action=Index}")]
    [AllowAnonymous]
    public class AuthenArea_BaseController : Controller
    {
        private EnterpriseRepo _organizationRepo;
        protected EnterpriseRepo organizationRepo
        {
            get
            {
                if (_organizationRepo == null)
                    _organizationRepo = new EnterpriseRepo(CompanyId);
                return _organizationRepo;
            }
        }
        
        public String CompanyId
        {
            get
            {
                var host = HttpContext.Request.Host.Host;
                var parts = host.Split('.');

                if (parts[0].StartsWith("localhost"))
                    parts[0] = "bit";

                return parts.Length > 0 ? parts[0] : host;
            }
        }
    }
}