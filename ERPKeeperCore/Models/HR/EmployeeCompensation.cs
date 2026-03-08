using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public enum EmployeeCompensationPeriod
    {
        Monthly = 0,
        Daily = 1,
        Weekly = 2,
        HalfMont = 3
    }
    public class EmployeeCompensation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public EmployeeCompensationPeriod Period { get; set; }

        public Decimal Amount { get; set; }


    }

}
