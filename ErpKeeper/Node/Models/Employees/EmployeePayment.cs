using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Taxes.Enums;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Employees
{


    [Table("ERP_Employee_Payments")]
    public class EmployeePayment
    {
        [Key]
        public Guid Uid { get; set; }
        public static ERPObjectType TransactionType = ERPObjectType.EmployeePayment;

        public int No { get; set; }
        public string Name => $"{DocumentCode}-{GetDocumentGroup()}/{No}";
        public string GetDocumentGroup() => EmployeePaymentPeriod?.Name;
        public string DocumentCode => Helpers.ObjectsHelper.TrCode(TransactionType);

        public EmployeePaymentStatus Status { get; set; }

        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public virtual FiscalYear FiscalYear { get; set; }

        public Guid? EmployeePaymentPeriodUid { get; set; }
        [ForeignKey("EmployeePaymentPeriodUid")]
        public virtual EmployeePaymentPeriod EmployeePaymentPeriod { get; set; }

        [Column("EmployeeUid")]
        public Guid EmployeeUid { get; set; }
        [ForeignKey("EmployeeUid")]
        public virtual Employees.Employee Employee { get; set; }

        public virtual ICollection<EmployeePaymentItem> PaymentItems { get; set; }

        public Decimal TotalEarning { get; set; }
        public Decimal TotalDeduction { get; set; }
        public Decimal TotalPayment => TotalEarning - TotalDeduction;

        public Guid? PayFromAccountGuid { get; set; }
        [ForeignKey("PayFromAccountGuid")]
        public virtual AccountItem PayFromAccount { get; set; }

        public EmployeePaymentItem addPaymentItems(EmployeePaymentType type, decimal amount)
        {
            var employeePaymentItem = new EmployeePaymentItem()
            {
                Uid = Guid.NewGuid(),
                PaymentType = type,
                PaymentTypeGuid = type.Uid,
                Amount = Math.Abs(amount)
            };

            PaymentItems.Add(employeePaymentItem);

            return employeePaymentItem;
        }

        public LedgerPostStatus PostStatus { get; set; }
        public void Calculate()
        {
            TotalEarning = PaymentItems
                .Where(pi => pi.PaymentType.PayDirection == PayDirection.Eanring)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();

            TotalDeduction = PaymentItems
                .Where(pi => pi.PaymentType.PayDirection == PayDirection.Deduction)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();
        }
    }

}

