using ERPKeeperCore.Enterprise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Views
{
    public abstract class Enterprise_BasePage<TModel> : RazorPage<TModel>
    {
        public String ApiUrl = @"";
        public Guid AuthorizeUserId => Guid.Parse(this.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        public String CompanyId =>  ViewContext.RouteData.Values["CompanyId"]?.ToString();
        public String GetRouteAtrribute(string key) => ViewContext.RouteData.Values[key]?.ToString();


        public bool IsOnEditMode => (!string.IsNullOrEmpty(Context.Request.Cookies["EditMode"]) && Context.Request.Cookies["EditMode"] == "Edit");



        private EnterpriseRepo _Organization;
        public EnterpriseRepo Organization
        {
            get
            {
                if (_Organization == null)
                    _Organization = new Enterprise.EnterpriseRepo(CompanyId,true);
                return _Organization;
            }
        }


        protected Enterprise_BasePage()
        {

        }
    }
}
