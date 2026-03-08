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

    public enum TimeAmount
    {
        FullDay = 0,
        HalfDay = 1,
    }

    public partial class EmployeeWorkRecord
    {
        [Key]
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.Now;
        
        public TimeAmount WorkAmount { get; set; } = TimeAmount.FullDay;

        public string? Notes { get; set; }
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
        public TimeAmount LeaveAmount { get; set; } = TimeAmount.FullDay;

        public LeaveAttendanceStatus Status { get; set; } = LeaveAttendanceStatus.Approved;

        public string? Notes { get; set; }

    }
}
