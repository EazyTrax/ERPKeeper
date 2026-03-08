using Microsoft.AspNetCore.Mvc;
using System;
using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Enterprise.Models.Employees.Enums;

namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("/Employee/{controller=Home}/{action=Index}/{id?}")]
    public class LeaveRecordsController : EmployeeBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveType type, LeaveAmount amount, DateTime recordDate, string notes = null)
        {
            try
            {
                var leaveRecord = new EmployeeLeaveRecord
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    leaveAmount = amount,
                    RecordDate = recordDate,
                    Status = LeaveAttendanceStatus.Pending,
                    Notes = notes,
                    EmployeeId = AuthorizeUserId
                };

                _dbContext.LeaveRecords.Add(leaveRecord);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the error
                return BadRequest($"Error creating leave record: {ex.Message}");
            }
        }


    }
}
