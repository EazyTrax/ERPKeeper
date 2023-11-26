using DevExpress.Web.Mvc;
using ERPKeeper.Node.Models.Employees;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    [RouteArea("Employees")]
    [RoutePrefix("Employee")]
    [Route("{id}/{action=Home}")]
    public class EmployeeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }

        public ActionResult DashBoard(Guid id)
        {
            var profile = Organization.Employees.Find(id);
            return View(profile);
        }

        public ActionResult Home(Guid id)
        {
            var employee = Organization.Employees.Find(id);
            employee.CalculateTotalIncome();
            Organization.SaveChanges();
            return View(employee);
        }

        public ActionResult Add(Guid id)
        {
            var profile = Organization.Profiles.Find(id);

            if (profile.ProfileType == ERPKeeper.Node.Models.Profiles.ProfileType.People)
            {
                var newEmployee = Organization.Employees.Create(profile);
                return RedirectToAction("Home", "Employee", new { id = newEmployee.ProfileUid, Area = "Employees" });
            }
            else
            {
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
        }

        public ActionResult Position(Guid id) => View(Organization.Employees.Find(id));
        public ActionResult Payments(Guid id) => View(Organization.Employees.Find(id));
        public ActionResult Leaves(Guid id) => View(Organization.Employees.Find(id));

        public ActionResult CreateLeave(Guid id)
        {
            var employee = Organization.Employees.Find(id);
            employee.CreateLeave(DateTime.Now, EmployeeLeaveType.FullDay);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        public PartialViewResult PartialGridViewPayments(Guid id)
        {
            var employeePayments = Organization.EmployeePayments.GetQuery()
                .Where(e => e.EmployeeUid == id)
                .ToList();

            return PartialView(employeePayments);
        }

        public PartialViewResult PartialGridViewLeaves(Guid id) => PartialView(Organization.Employees.Find(id));


        public ActionResult Activities(Guid id)
        {
            var profile = Organization.Employees.Find(id);
            return View("Activities", profile);
        }



        public ActionResult ChangePosition(ERPKeeper.Node.Models.Employees.Employee employee)
        {
            var existEmployee = Organization.Employees.Find(employee.ProfileUid);

            existEmployee.PositionGuid = employee.PositionGuid;

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public static MVCxAppointmentStorage CreateStorage()
        {
            MVCxAppointmentStorage appStorage = new MVCxAppointmentStorage();
            appStorage.Mappings.AppointmentId = "Uid";
            appStorage.Mappings.Start = "TimeStarted";
            appStorage.Mappings.End = "EndDate";
            appStorage.Mappings.Subject = "Title";
            appStorage.Mappings.Description = "Home";
            appStorage.Mappings.Type = "EventType";
            appStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            appStorage.Mappings.Label = "Label";
            appStorage.Mappings.Status = "DevexpressStatus";
            return appStorage;
        }

        public ActionResult NewPayment(Guid id)
        {
            var employeePayment = Organization.EmployeePayments.CreateNew(id, DateTime.Now);

            return RedirectToAction("Home", "Payment", new
            {
                Area = "Employees",
                id = employeePayment.Uid
            });
        }

        public ActionResult MarkInActive(Guid id)
        {
            var employee = Organization.Employees.Find(id);
            employee.Status = ERPKeeper.Node.Models.Employees.EmployeeStatus.InActive;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult MarkActive(Guid id)
        {
            var employee = Organization.Employees.Find(id);
            employee.Status = ERPKeeper.Node.Models.Employees.EmployeeStatus.Active;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        
        public ActionResult Remove(Guid id)
        {
            Organization.Employees.Delete(id);

            return RedirectToAction("Home", "Employees");
        }


    }
}