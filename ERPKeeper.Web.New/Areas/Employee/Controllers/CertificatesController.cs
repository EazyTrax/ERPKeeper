using Microsoft.AspNetCore.Mvc;
using System;

namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("/Employee/{controller=Home}/{action=Index}/{id?}")]
    public class CertificatesController : EmployeeBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
