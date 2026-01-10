using ERPKeeperCore.Enterprise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.API.Profile.Views.Shared
{
    public abstract class Profile_Profile_BasePage<TModel> : Web.Views.Enterprise_BasePage<TModel>
    {
        public Guid ProfileUid => Guid.Parse(ViewContext.RouteData.Values["ProfileUid"]?.ToString());



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
