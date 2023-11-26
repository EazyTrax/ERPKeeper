using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Taxes.Enums;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Employees
{

    [Table("ERP_Employee_Payments")]
    public class EmployeePayment
    {
        [Key]
        public Guid Id { get; set; }
        public static ERPObjectType TransactionType = ERPObjectType.EmployeePayment;

        public int No { get; set; }
        public string Name => $"{DocumentCode}-{GetDocumentGroup()}/{No}";
        public string GetDocumentGroup() => EmployeePaymentPeriod?.Name;
        public string DocumentCode => Helpers.ObjectsHelper.TrCode(TransactionType);

        public EmployeePaymentStatus Status { get; set; }

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual FiscalYear FiscalYear { get; set; }

        public Guid? EmployeePaymentPeriodId { get; set; }
        [ForeignKey("EmployeePaymentPeriodId")]
        public virtual EmployeePaymentPeriod EmployeePaymentPeriod { get; set; }

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public Decimal TotalEarning { get; set; }
        public Decimal TotalDeduction { get; set; }
        public Decimal TotalPayment => TotalEarning - TotalDeduction;

        public Guid? PayFromAccountId { get; set; }
        [ForeignKey("PayFromAccountId")]
        public virtual AccountItem PayFromAccount { get; set; }

        public virtual ICollection<EmployeePaymentLine> EmployeePaymentLines { get; set; }





        public EmployeePaymentLine addPaymentItems(EmployeePaymentType type, decimal amount)
        {
            var employeePaymentItem = new EmployeePaymentLine()
            {
                Id = Guid.NewGuid(),
                PaymentType = type,
                PaymentTypeId = type.Id,
                Amount = Math.Abs(amount)
            };

            EmployeePaymentLines.Add(employeePaymentItem);

            return employeePaymentItem;
        }

        public LedgerPostStatus PostStatus { get; set; }
        public void Calculate()
        {
            TotalEarning = EmployeePaymentLines
                .Where(pi => pi.PaymentType.PayDirection == PayDirection.Eanring)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();

            TotalDeduction = EmployeePaymentLines
                .Where(pi => pi.PaymentType.PayDirection == PayDirection.Deduction)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();
        }
    }

}

