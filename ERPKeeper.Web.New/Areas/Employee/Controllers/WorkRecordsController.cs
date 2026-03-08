using Microsoft.AspNetCore.Mvc;
using System;
using ERPKeeperCore.Enterprise.Models.Employees;

namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("/Employee/{controller=Home}/{action=Index}/{id?}")]
    public class WorkRecordsController : EmployeeBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TimeAmount amount, DateTime recordDate, string notes = null)
        {
            try
            {
                var workRecord = new EmployeeWorkRecord
                {
                    Id = Guid.NewGuid(),
                    RecordDate = recordDate,
                    WorkAmount = amount,
                    Notes = notes,
                    EmployeeId = AuthorizeUserId
                };

                _dbContext.WorkRecords.Add(workRecord);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating work record: {ex.Message}");
            }
        }

    }
}

