using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public class EmployeeDevelopmentCourse
    {
        [Key]
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

    }
}
