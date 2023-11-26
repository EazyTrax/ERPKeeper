using ERPKeeper.Node.Models.Employees;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    public class EmployeesController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult Home(EmployeeStatus? id)
        {
            if (id == null)
                return RedirectToAction("Home", new { Id = EmployeeStatus.Active });
            else
                ViewBag.id = id;

            return View();
        }

        public ActionResult UpdateAmount()
        {
            return RedirectToAction("Home");
        }

        [ValidateInput(false)]
        public PartialViewResult PartialGridView(EmployeeStatus id)
        {
            ViewBag.id = id;
            return PartialView("PartialGridView", Organization.Employees.GetAll().Where(e => e.Status == id).ToList());
        }
    }
}