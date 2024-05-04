using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Suppliers
{
    public class SupplierPayment
    {
        [Key]
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }

        public bool IsPosted { get; set; } 
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }


        public Guid PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual Purchase? Purchase { get; set; }


        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Decimal Total { get; set; }



        public Guid? PayFromAccountId { get; set; }
        [ForeignKey("PayFromAccountId")]
        public virtual Accounting.Account? PayFromAccount { get; set; }

        public Guid? RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual Financial.RetentionType? RetentionType { get; set; }


        public void CreateTransaction()
        {
            if (this.Transaction == null)
            {
                Console.WriteLine("Creating Transaction");
                this.Transaction = new Accounting.Transaction()
                {
                    Id = this.Id,
                    Date = this.Date,
                    Reference = this.Name,
                    Type = Accounting.Enums.TransactionType.SupplierPayment,
                    SupplierPayment = this,
                };
            }
            this.Transaction.ClearLedger();
            this.Transaction.UpdateBalance();
        }
    }
}
