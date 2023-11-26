using ERPKeeper.Node.Models.Security.Enums;
using ERPKeeper.WebFrontEnd.Authorize;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Controllers
{
    [ERPAuthorize(AccessLevel.Viewer)]

    public class _DefaultNodeController : Controller
    {
        public Node.DAL.Organization Organization { get; private set; }
        private Node.DBContext.ERPDbContext erpNodeDBContext { get; set; }
        public Library.ERPKeeperUser CurrentUser => new Library.ERPKeeperUser(this.User as ClaimsPrincipal);
        public DateTime WorkingDate => DateTime.Parse(this.HttpContext.Session["WorkingDate"]?.ToString() ?? DateTime.Today.ToShortDateString());

        protected _DefaultNodeController() : base()
        {

        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.ConnectDbcontext(requestContext);


            ViewBag.erpNodeDBContext = this.Organization.ErpNodeDBContext;
            ViewBag.Organization = this.Organization;
        }

        private void ConnectDbcontext(RequestContext requestContext)
        {
            try
            {
                string nodeName = requestContext.HttpContext.Request.Url.Host.ToLower();

                if (nodeName == "localhost")
                    nodeName = "tec";

                ViewBag.nodeName = nodeName;

                Organization = new ERPKeeper.Node.DAL.Organization(nodeName);
                erpNodeDBContext = Organization.ErpNodeDBContext;
            }
            catch (ERPKeeper.Node.DBContext.DatabaseNotFoundException ex)
            {
                throw new HttpException(404, ex.ToString());
            }
        }
    }
}