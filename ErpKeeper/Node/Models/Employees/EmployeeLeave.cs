using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeper.Node.Models.Employees
{

    public enum EmployeeLeaveType
    {
        FullDay = 0,
        HalfDay = 1,
        Last = 2,
        Other = 10
    }

    [Table("ERP_Employee_Leaves")]
    public class EmployeeLeave
    {
        [Key]
        public Guid Uid { get; set; }

        public int? No { get; set; }

        public string Name => string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No?.ToString().PadLeft(3, '0'));

        public EmployeeLeaveType Type { get; set; }

        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }

        public int AmountDay { get; set; }
        public int AmountHour { get; set; }


        [Column("EmployeeUid")]
        public Guid EmployeeUid { get; set; }
        [ForeignKey("EmployeeUid")]
        public virtual Employees.Employee Employee { get; set; }
    }


}