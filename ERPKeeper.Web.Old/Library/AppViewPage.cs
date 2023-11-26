using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;


namespace ERPKeeper.WebFrontEnd.Library
{

    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        public Node.DAL.Organization Organization => (Node.DAL.Organization)ViewBag.Organization;
        public Node.DBContext.ERPDbContext erpNodeDBContext => (Node.DBContext.ERPDbContext)ViewBag.erpNodeDBContext;
        public ERPKeeperUser CurrentUser => new Library.ERPKeeperUser(this.User as ClaimsPrincipal);
        public DateTime WorkingDate => DateTime.Parse(this.Session["WorkingDate"]?.ToString() ?? DateTime.Today.ToShortDateString());
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {

    }
}