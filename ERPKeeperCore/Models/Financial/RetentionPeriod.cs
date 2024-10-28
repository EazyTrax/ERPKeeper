using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Financial.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERPKeeperCore.Enterprise.Models.Financial
{
    [Table("RetentionPeriods")]
    public class RetentionPeriod
    {
        [Key]
        public Guid Id { get; set; }


        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }



        public DateTime Date { get; set; }
        public DateTime StartDate => new DateTime(Date.Year, Date.Month, 1);
        public DateTime EndDate => new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month));



        public Guid RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual RetentionType RetentionType { get; set; }

        public virtual ICollection<ReceivePayment> ReceivePayments { get; set; } = new List<ReceivePayment>();
        public virtual ICollection<SupplierPayment> SupplierPayments { get; set; } = new List<SupplierPayment>();

        public decimal AmountCommercial { get; set; }
        public decimal AmountRetention { get; set; }
        public int Count { get; set; }
        public int No { get; internal set; }




        public DateTime? PayDate { get; set; }
        public Guid? PayFromAccountId { get; set; }
        [ForeignKey("PayFromAccountId")]
        public virtual Accounting.Account? PayFromAccount { get; set; }


        public DateTime? PostedDate { get; set; }
        public string? Name { get; set; }
        public string? Reference { get; set; }

        public void Calculate()
        {
            Count = ReceivePayments.Count() + SupplierPayments.Count();

            AmountCommercial =
                ReceivePayments.Select(t => t.AmountTotal).DefaultIfEmpty(0).Sum() +
                SupplierPayments.Select(t => t.AmountAfterDiscount).DefaultIfEmpty(0).Sum();

            AmountRetention =
                 ReceivePayments.Select(t => t.AmountRetention).DefaultIfEmpty(0).Sum() +
                 SupplierPayments.Select(t => t.AmountRetention).DefaultIfEmpty(0).Sum();
        }

        public void PostToTransaction()
        {
            this.Calculate();

            if (this.AmountRetention > 0)
                Console.WriteLine($">Post  RetentionGroup:{this.Name} {this.Count}  {this.AmountRetention}");
            else
                return;

            if (this.Transaction == null || this.PayDate == null || this.PayFromAccount == null)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = (DateTime)this.PayDate;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Today;

            // Dr.
            this.Transaction.AddDebit(this.RetentionType.RetentionToAccount, this.AmountRetention);

            // Cr.
            this.Transaction.AddCredit(this.PayFromAccount, this.AmountRetention);



            IsPosted = this.Transaction.UpdateBalance();

        }
    }


}