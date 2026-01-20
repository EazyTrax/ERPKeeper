using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Profile.Controllers
{
    [Area("Profiles_Profile")]
    public class Profiles_Profile_BaseController : _BaseController
    {
        public Guid ProfileUid => Guid.Parse(RouteData.Values["ProfileUid"]?.ToString());


        public ERPKeeperCore.Enterprise.Models.Profiles.Profile _Profile;
        public ERPKeeperCore.Enterprise.Models.Profiles.Profile Profile
        {
            get
            {
                if (_Profile == null)
                    _Profile = Organization.Profiles.Find(ProfileUid);
                return _Profile;
            }
        }


    }
}