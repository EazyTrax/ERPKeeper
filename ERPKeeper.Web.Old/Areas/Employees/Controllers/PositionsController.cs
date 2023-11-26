using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    public class PositionsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }



        public ActionResult Home() => View();

        [AllowAnonymous]
        public PartialViewResult _AvailablePositions()
        {
            return PartialView();
        }

        [ValidateInput(false)]
        public PartialViewResult PartialGridView()
        {
            return PartialView("PartialGridView", Organization.EmployeePositions.GetAll());
        }
    }
}