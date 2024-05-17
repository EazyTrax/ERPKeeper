
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


        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }


        public int No { get; set; }
        public string Name { get; set; }



        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees.Employee Employee { get; set; }

        public Guid? EmployeePaymentPeriodId { get; set; }
        [ForeignKey("EmployeePaymentPeriodId")]
        public virtual EmployeePaymentPeriod EmployeePaymentPeriod { get; set; }



        public Decimal TotalEarning { get; set; }
        public Decimal TotalDeduction { get; set; }
        public Decimal TotalPayment => TotalEarning - TotalDeduction;

        public Guid? PayFromAccountId { get; set; }
        [ForeignKey("PayFromAccountId")]
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


        public void UpdateBalance()
        {
            TotalEarning = PaymentItems
                .Where(pi => pi.EmployeePaymentType.PayDirection == PayDirection.Eanring)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();

            TotalDeduction = PaymentItems
                .Where(pi => pi.EmployeePaymentType.PayDirection == PayDirection.Deduction)
                .Select(l => l.Amount).DefaultIfEmpty(0).Sum();
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post >EmployeePayment:{this.Name}");

            if (this.Transaction == null) return;
            this.Transaction.ClearLedger();
            this.IsPosted = false;
            this.Transaction.Date = this.EmployeePaymentPeriod.Date;
            this.Transaction.Reference = this.Name;
            this.Transaction.PostedDate = DateTime.Now;

            //Prepare Data

            this.UpdateBalance();
            var earningItems = this.PaymentItems
                .Where(i => i.EmployeePaymentType.PayDirection == PayDirection.Eanring)
                .ToList();
            var dedectionItems = this.PaymentItems
                .Where(i => i.EmployeePaymentType.PayDirection == PayDirection.Deduction)
                .ToList();

            //Dr.
            foreach (var paymentItem in earningItems)
                this.Transaction.AddDebit(paymentItem.EmployeePaymentType.ExpenseAccount, paymentItem.Amount);
            //Cr.
            this.Transaction.AddCredit(this.EmployeePaymentPeriod.PayFromAccount, this.TotalEarning - this.TotalDeduction);

            foreach (var paymentItem in dedectionItems)
                this.Transaction.AddCredit(paymentItem.EmployeePaymentType.ExpenseAccount, paymentItem.Amount);

            IsPosted = this.Transaction.UpdateBalance();
        }
    }
}

