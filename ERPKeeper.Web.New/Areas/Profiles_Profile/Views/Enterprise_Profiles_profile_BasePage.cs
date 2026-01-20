using Azure.Core;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Web.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Profile.Views
{
    public abstract class Enterprise_Profiles_profile_BasePage<TModel> : Enterprise_BasePage<TModel>
    {
        public Guid ProfileId => Guid.Parse(ViewContext.RouteData.Values["ProfileUid"]?.ToString());

        private ERPKeeperCore.Enterprise.Models.Profiles.Profile _Profile;
        public ERPKeeperCore.Enterprise.Models.Profiles.Profile Profile
        {
            get
            {
                if (_Profile == null)
                    _Profile = Organization.Profiles.Find(ProfileId);
                return _Profile;
            }
        }

        protected Enterprise_Profiles_profile_BasePage()
        {

        }
    }
}
