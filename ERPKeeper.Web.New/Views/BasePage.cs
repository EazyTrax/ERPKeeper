using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Views
{
    public abstract class BasePage<TModel> : RazorPage<TModel>
    {
        public String ApiUrl = @"";
        public Guid CurrentMakerId => Guid.Parse(this.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        public String CompanyId =>  ViewContext.RouteData.Values["CompanyId"]?.ToString();
        public String GetRouteAtrribute(string key) => ViewContext.RouteData.Values[key]?.ToString();

        public bool IsOnEditMode = true;

        private ERPKeeper.Node.DAL.Organization _Organization;
        public ERPKeeper.Node.DAL.Organization Organization
        {
            get
            {
                if (_Organization == null)
                    _Organization = new Node.DAL.Organization(CompanyId);
                return _Organization;
            }
        }


        protected BasePage()
        {

        }
    }
}
