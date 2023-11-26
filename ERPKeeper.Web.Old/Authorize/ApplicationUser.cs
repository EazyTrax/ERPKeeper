using ERPKeeper.Node.Models.Profiles;
using ERPKeeper.Node.Models.Security.Enums;
using System;
using System.Linq;
using System.Security.Claims;

namespace ERPKeeper.WebFrontEnd.Library
{

    public enum OptionalClaimType
    {
        AccessLevel = 0,
        TaxId = 1,
        ViewPeriod = 2
    }

    public class ERPKeeperUser : ClaimsPrincipal
    {
        public ERPKeeperUser(ClaimsPrincipal principal) : base(principal)
        {
        }

        public String Name => this.FindFirst(ClaimTypes.Name).Value;
        public String GivenName => this.FindFirst(ClaimTypes.GivenName).Value;
        public String Email => this.FindFirst(ClaimTypes.Email).Value;

        public AccessLevel AccessLevel
        {
            get
            {
                var accessLevelString = this.FindFirst(OptionalClaimType.AccessLevel.ToString())?.Value.ToString() ?? AccessLevel.None.ToString();
                return (AccessLevel)Enum.Parse(typeof(AccessLevel), accessLevelString);
            }
        }

        public ViewPeriod CurrentViewPeriod
        {
            get
            {
                string DefaulViewPeriodString = ViewPeriod.Week.ToString();
                var accessLevelString = this.FindFirst(OptionalClaimType.ViewPeriod.ToString())?.Value.ToString() ?? DefaulViewPeriodString;
                return (ViewPeriod)Enum.Parse(typeof(ViewPeriod), accessLevelString);
            }
        }


        public bool CheckAccessLevelAuthorize(AccessLevel requriedAccessLevel)
        {
            if ((int)this.AccessLevel >= (int)requriedAccessLevel)
                return true;
            else
                return false;
        }


        public String NameIdentifier => this.FindFirst(ClaimTypes.NameIdentifier).Value.ToLower();
        public String TaxId => this.FindFirst(OptionalClaimType.TaxId.ToString()).Value;
        public Guid ProfileUid => Guid.Parse(this.FindFirst(ClaimTypes.NameIdentifier).Value ?? Guid.Empty.ToString());

    }
}