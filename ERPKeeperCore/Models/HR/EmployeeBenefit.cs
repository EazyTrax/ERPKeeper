using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public enum BenefitType
    {
        HealthInsurance,
        RetirementPlan,
        PaidTimeOff,
        Other
    }
    public class EmployeeBenefit
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public String Description { get; set; }
        public Decimal Amount { get; set; }
    }
}
