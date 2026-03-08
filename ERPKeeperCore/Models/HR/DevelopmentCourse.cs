using System.ComponentModel.DataAnnotations;

namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public class DevelopmentCourse
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Requried { get; set; }

        public virtual ICollection<EmployeeDevelopmentCourse> EmployeeDevelopmentCourses { get; set; } = new List<EmployeeDevelopmentCourse>();

    }
}
