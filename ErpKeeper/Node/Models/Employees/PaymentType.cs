using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ERPKeeper.Node.Models.Employees
{

    [Table("ERP_Employee_Payment_Types")]
    public class EmployeePaymentType
    {
        [Key]
        public Guid Uid { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }

        public PayDirection PayDirection { get; set; }

        public Guid? AccountGuid { get; set; }
        [ForeignKey("AccountGuid")]
        public virtual Models.Accounting.AccountItem Account { get; set; }

        public Guid? RetentionTypeGuid { get; set; }
        [ForeignKey("RetentionTypeGuid")]
        public virtual Models.Financial.Payments.RetentionType RetentionType { get; set; }
        public virtual ICollection<EmployeePaymentItem> Payments { get; set; }

        public void Update(EmployeePaymentType paymentType)
        {
            this.Name = paymentType.Name;
            this.PayDirection = paymentType.PayDirection;
            this.Description = paymentType.Description;
            this.AccountGuid = paymentType.AccountGuid;
        }
    }
}
