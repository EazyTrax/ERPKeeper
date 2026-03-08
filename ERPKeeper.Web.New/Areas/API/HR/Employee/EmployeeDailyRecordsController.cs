using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.Areas.API.Profiles.HR.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ERPKeeperCore.Web.Areas.API.HR.Employee
{
    // Support both old and new route names for backward compatibility
    [Route("/API/HR/Employees/{EmployeeId:Guid}/EmployeeDailyRecords/{action=Index}")]
    public class EmployeeDailyRecordsController : _API_HR_Employee_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.LeaveRecords
                .Where(a => a.EmployeeId == EmployeeId)
                .OrderByDescending(a => a.RecordDate)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.EmployeeLeaveRecord();
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            model.Id = Guid.NewGuid();
            model.EmployeeId = EmployeeId;
            
            // Set default type to WorkNormal if not specified
            if (model.Type == 0)
                model.Type = Enterprise.Models.Employees.LeaveType.Absent;
            
            // Set default status to Approved if not specified
            if (model.Status == 0)
                model.Status = Enterprise.Models.Employees.LeaveAttendanceStatus.Approved;

            Organization.ErpCOREDBContext.LeaveRecords.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.LeaveRecords
                .First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.LeaveRecords
                .First(a => a.Id == key);
            Organization.ErpCOREDBContext.LeaveRecords
                .Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }

        /// <summary>
        /// Generate attendance records for a date range
        /// </summary>
        [HttpPost]
        public IActionResult GenerateAttendance(DateTime startDate, DateTime endDate)
        {
            var existingDates = Organization.ErpCOREDBContext.LeaveRecords
                .Where(a => a.EmployeeId == EmployeeId && a.RecordDate >= startDate && a.RecordDate <= endDate)
                .Select(a => a.RecordDate.Date)
                .ToList();

            var currentDate = startDate.Date;
            var addedCount = 0;

            while (currentDate <= endDate.Date)
            {
                if (!existingDates.Contains(currentDate))
                {
                    var attendance = new Enterprise.Models.Employees.EmployeeLeaveRecord
                    {
                        Id = Guid.NewGuid(),
                        EmployeeId = EmployeeId,
                        RecordDate = currentDate,
                        Type = Enterprise.Models.Employees.LeaveType.Absent,
                        Status = Enterprise.Models.Employees.LeaveAttendanceStatus.Approved
                    };

                    Organization.ErpCOREDBContext.LeaveRecords.Add(attendance);
                    addedCount++;
                }

                currentDate = currentDate.AddDays(1);
            }

            Organization.ErpCOREDBContext.SaveChanges();

            return Ok(new { success = true, recordsAdded = addedCount });
        }
    }
}
