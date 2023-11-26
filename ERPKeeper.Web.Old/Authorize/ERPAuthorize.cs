using System;
using System.Web;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Authorize
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class ERPAuthorize : AuthorizeAttribute, IDisposable
    {
        Library.ERPKeeperUser currentUser { get; set; }
        ERPKeeper.Node.DBContext.ERPDbContext erpNodeDBContext { get; set; }
        private ERPKeeper.Node.Models.Security.Enums.AccessLevel requiredAccessLevel { get; set; }


        public ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel requiredAccessLevel)
        {
            this.requiredAccessLevel = requiredAccessLevel;
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
                return false;
            else if (!this.VerifyAccessLevel(httpContext))
                return false;
            else
                return true;
        }

        private bool VerifyAccessLevel(HttpContextBase httpContext)
        {
            this.currentUser = new Library.ERPKeeperUser(httpContext.User as System.Security.Claims.ClaimsPrincipal);
            if ((int)currentUser.AccessLevel < (int)requiredAccessLevel)
                return false;
            else
                return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                     new System.Web.Routing.RouteValueDictionary(
                         new { Area = "Authen", Controller = "UnAuthorize", Action = "AccessLevel" }));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
        public void Dispose()
        {
            if (erpNodeDBContext != null)
            {
                erpNodeDBContext.Dispose();
                erpNodeDBContext = null;
            }
        }
    }
}