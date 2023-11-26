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
using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeper.Web.New.API
{
    [Area("API")]
    [Route("/API/{CompanyId}/{controller=Home}/{action=Index}/{id?}")]
    public class APIBaseController : Controller
    {
        public Guid CurrentMakerId => Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public String CompanyId => HttpContext.GetRouteData().Values["CompanyId"].ToString();

        private ERPKeeper.Node.DAL.Organization _Organization;
        public ERPKeeper.Node.DAL.Organization Organization
        {
            get
            {
                if (_Organization == null)
                    _Organization = new Node.DAL.Organization(CompanyId);

                _Organization.ErpNodeDBContext.Configuration.LazyLoadingEnabled = false;
                return _Organization;
            }
        }


        public APIBaseController()
        {

        }
    }
}