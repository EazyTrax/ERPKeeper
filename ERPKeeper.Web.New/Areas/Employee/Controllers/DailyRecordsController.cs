using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Enterprise.Models.Employees.Enums;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("/Employee/{controller=Home}/{action=Index}/{id?}")]
    public class DailyRecordsController : _BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AutoDailyRecords()
        {
            try
            {
                var employees = _dbContext.Employees
                    .Where(e => e.Status == EmployeeStatus.Active)
                    .ToList();
                
                if (!employees.Any())
                {
                    TempData["ErrorMessage"] = "No active employees found.";
                    return RedirectToAction("Index");
                }

                var startDate = new DateTime(DateTime.Today.Year, 1, 1);
                var endDate = new DateTime(DateTime.Today.Year, 12, 31).AddDays(1);
                int createdRecords = 0;
                int skippedRecords = 0;

                foreach (var employee in employees)
                {
                    for (var date = startDate; date < endDate; date = date.AddDays(1))
                    {
                        // Check if record already exists
                        var existingRecord = _dbContext.EmployeeDailyRecords
                            .FirstOrDefault(r => r.EmployeeId == employee.Id && r.RecordDate.Date == date.Date);

                        if (existingRecord != null)
                        {
                            skippedRecords++;
                            continue;
                        }

                        // Create new record
                        var dailyRecord = new EmployeeDailyRecord
                        {
                            Id = Guid.NewGuid(),
                            EmployeeId = employee.Id,
                            RecordDate = date,
                            Status = AttendanceStatus.Approved
                        };

                        // Check if it's Saturday or Sunday and mark as Holiday
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            dailyRecord.Type = AttendanceType.Holiday;
                            dailyRecord.Notes = "Weekend";
                        }
                        else
                        {
                            dailyRecord.Type = AttendanceType.WorkNormal;
                        }

                        _dbContext.EmployeeDailyRecords.Add(dailyRecord);
                        createdRecords++;
                    }
                }

                _dbContext.SaveChanges();
                
                TempData["SuccessMessage"] = $"Successfully created {createdRecords} daily records for {employees.Count} employees. Skipped {skippedRecords} existing records.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error creating auto daily records: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
