using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPKeeperCore.Enterprise.Models.Employees
{

    public enum AttendanceType
    {
        WorkNormal,
        LeaveWithPay,
        LeaveWithoutPay,
        Sick,
        Vacation,
        Holiday,
        Absent
    }

    public enum AttendanceStatus
    {
        Approved,
        Pending,
        Rejected
    }

    public partial class EmployeeDailyRecord
    {
        [Key]
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.Now;

        public AttendanceType Type { get; set; } = AttendanceType.WorkNormal;
        
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Approved;

        public string? Notes { get; set; }

        // Helper property to identify if it's a leave day
        [NotMapped]
        public bool IsLeave => Type != AttendanceType.WorkNormal && Type != AttendanceType.Holiday;

        // Helper property to identify if it's a work day
        [NotMapped]
        public bool IsWorkDay => Type == AttendanceType.WorkNormal;
    }
}
