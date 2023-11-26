using ERPKeeper.Node.Models.Employees;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    [RouteArea("Employees")]
    [RoutePrefix("Position")]
    [Route("{id}/{action=Home}")]
    public class PositionController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var position = Organization.EmployeePositions.Find(id);

            if (position == null)
                return RedirectToAction("Home", "Positions", new { Area = "Employees" });
            else
                return View(position);
        }
        public ActionResult Employees(Guid id)
        {
            var position = Organization.EmployeePositions.Find(id);
            return View(position);
        }

        public ActionResult New()
        {
            var newPosition = new EmployeePosition();
            return View("Home", newPosition);
        }

        public ActionResult Save(ERPKeeper.Node.Models.Employees.EmployeePosition position)
        {
            var existPosition = Organization.EmployeePositions.Find(position.Uid);

            if (existPosition != null)
                existPosition.Update(position);
            else
                existPosition = Organization.EmployeePositions.CreateNew(position);

            Organization.SaveChanges();

            return RedirectToAction("Home", new { id = existPosition.Uid });

        }
    }

}