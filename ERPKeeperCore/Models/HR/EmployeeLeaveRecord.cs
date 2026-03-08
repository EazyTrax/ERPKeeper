using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public enum LeaveType
    {
        Sick,
        Vacation,
        Absent
    }

    public enum LeaveAttendanceStatus
    {
        Approved,
        Pending,
        Rejected
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

        public LeaveAttendanceStatus Status { get; set; } = LeaveAttendanceStatus.Approved;

        public string? Notes { get; set; }

    }
}
