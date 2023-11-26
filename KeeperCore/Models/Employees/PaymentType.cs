using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace KeeperCore.ERPNode.Models.Employees
{

    [Table("ERP_Employee_Payment_Types")]
    public class EmployeePaymentType
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }

        public PayDirection PayDirection { get; set; }

        public Guid? ExpenseAccountId { get; set; }
        [ForeignKey("ExpenseAccountId")]
        public virtual Models.ChartOfAccount.AccountItem ExpenseAccount { get; set; }

        public virtual ICollection<EmployeePaymentLine> EmployeePaymentLines { get; set; }

        public void Update(EmployeePaymentType paymentType)
        {
            this.Name = paymentType.Name;
            this.PayDirection = paymentType.PayDirection;
            this.Description = paymentType.Description;
            this.ExpenseAccountId = paymentType.ExpenseAccountId;
        }
    }
}
