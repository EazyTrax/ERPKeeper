using ERPKeeperCore.Enterprise.Models.Employees.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ERPKeeperCore.Enterprise.Models.Employees
{

    public class EmployeePaymentType
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }

        public PayDirection PayDirection { get; set; }

        public Guid? AccountUid { get; set; }
        [ForeignKey("AccountUid")]
        public virtual Models.Accounting.Account Account { get; set; }

        public virtual ICollection<EmployeePaymentItem> Payments { get; set; }

        public void Update(EmployeePaymentType paymentType)
        {
            this.Name = paymentType.Name;
            this.PayDirection = paymentType.PayDirection;
            this.Description = paymentType.Description;
            this.AccountUid = paymentType.AccountUid;
        }
    }
}
