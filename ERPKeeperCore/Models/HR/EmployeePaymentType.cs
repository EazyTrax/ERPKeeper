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
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public PayDirection PayDirection { get; set; }

        public Guid? ExpenseAccountId { get; set; }
        [ForeignKey("ExpenseAccountId")]
        public virtual Models.Accounting.Account ExpenseAccount { get; set; }

        public virtual ICollection<EmployeePaymentItem> Items { get; set; }
        public decimal Total { get; private set; }

        public void UpdateBalance()
        {
            this.Total= Items.Sum(a => a.Amount);
        }
        public void Update(EmployeePaymentType paymentType)
        {
            this.Name = paymentType.Name;
            this.PayDirection = paymentType.PayDirection;
            this.Description = paymentType.Description;
            this.ExpenseAccount = paymentType.ExpenseAccount;
        }
    }
}
