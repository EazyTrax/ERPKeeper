
using ERPKeeperCore.Enterprise.Models.Employees.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public class EmployeePayment
    {
        [Key]
        public Guid Id { get; set; }

        public int No { get; set; }
        public string Name => $"EP-{GetDocumentGroup()}/{No}";
        public string GetDocumentGroup() => "";

        [Column("EmployeeUid")]
        public Guid EmployeeUid { get; set; }
        [ForeignKey("EmployeeUid")]
        public virtual Employees.Employee Employee { get; set; }

        public Guid? EmployeePaymentPeriodUid { get; set; }
        [ForeignKey("EmployeePaymentPeriodUid")]
        public virtual EmployeePaymentPeriod EmployeePaymentPeriod { get; set; }



        public Decimal TotalEarning { get; set; }
        public Decimal TotalDeduction { get; set; }
        public Decimal TotalPayment => TotalEarning - TotalDeduction;

        public Guid? PayFromAccountUid { get; set; }
        [ForeignKey("PayFromAccountUid")]
        public virtual Accounting.Account PayFromAccount { get; set; }
        public virtual ICollection<EmployeePaymentItem> PaymentItems { get; set; }

        public EmployeePaymentItem addPaymentItems(EmployeePaymentType type, decimal amount)
        {
            var employeePaymentItem = new EmployeePaymentItem()
            {
                
                EmployeePaymentType = type,
                EmployeePaymentId = type.Id,
                Amount = Math.Abs(amount)
            };

            PaymentItems.Add(employeePaymentItem);

            return employeePaymentItem;
        }

     
        public void Calculate()
        {
            TotalEarning = PaymentItems
                .Where(pi => pi.EmployeePaymentType.PayDirection == PayDirection.Eanring)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();

            TotalDeduction = PaymentItems
                .Where(pi => pi.EmployeePaymentType.PayDirection == PayDirection.Deduction)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();
        }
    }

}

