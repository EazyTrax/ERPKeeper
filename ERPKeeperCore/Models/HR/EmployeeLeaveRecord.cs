using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public enum LeaveType
    {
        Sick = 0,
        Absent = 1
    }

    public enum LeaveAttendanceStatus
    {
        Approved,
        Pending,
        Rejected
    }

    public enum LeaveAmount
    {
        FullDay = 0,
        HalfDay = 1,
    }



    public partial class EmployeeLeaveRecord
    {
        [Key]
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.Now;

        public LeaveType Type { get; set; } = LeaveType.Absent;
        public LeaveAmount leaveAmount { get; set; } = LeaveAmount.FullDay;

        public LeaveAttendanceStatus Status { get; set; } = LeaveAttendanceStatus.Approved;

        public string? Notes { get; set; }

    }
}
