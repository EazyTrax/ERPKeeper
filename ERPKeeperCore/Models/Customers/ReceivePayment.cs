using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Customers
{
    public class ReceivePayment
    {
        [Key]
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }

        public Guid? SaleId { get; set; }
        [ForeignKey("SaleId")]
        public virtual Sale? Sale { get; set; }

        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;


        public Decimal Amount { get; set; }
        public Decimal AmountExcludeTax { get; set; }

        public Decimal AmountRetention { get; set; }
        public Decimal AmountDiscount { get; set; }
        public Decimal AmountBankFee { get; set; }
        public Decimal AmountTotalReceive =>
            Amount - (AmountRetention + AmountDiscount + AmountBankFee);


        public Guid? PayToAccountId { get; set; }
        [ForeignKey("PayToAccountId")]
        public virtual Accounting.Account? PayToAccount { get; set; }

        public Guid? ReceivableAccountId { get; set; }
        [ForeignKey("ReceivableAccountId")]
        public virtual Accounting.Account? ReceivableAccount { get; set; }


        public Guid? RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual Financial.RetentionType? RetentionType { get; set; }

        public Guid? DiscountAccountId { get; set; }
        [ForeignKey("DiscountAccountId")]
        public virtual Accounting.Account? DiscountAccount { get; set; }

        public Guid? BankFeeAccountId { get; set; }
        [ForeignKey("BankFeeAccountId")]
        public virtual Accounting.Account? BankFeeAccount { get; set; }


        public void PostToTransaction()
        {
            Console.WriteLine($">Post  RP:{this.Name}");

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();

            // Dr.
            this.Transaction.AddDebit(this.PayToAccount, this.AmountTotalReceive);

            if (this.AmountDiscount > 0)
                this.Transaction.AddDebit(this.DiscountAccount, this.AmountDiscount);
            if (this.RetentionTypeId != null)
                this.Transaction.AddDebit(this.RetentionType.RetentionToAccount, this.AmountRetention);
            if (this.AmountBankFee > 0)
                this.Transaction.AddDebit(this.BankFeeAccount, this.AmountBankFee);



            // Cr.
            this.Transaction.AddCredit(this.ReceivableAccount, this.Amount);

            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Now;
            this.Transaction.UpdateBalance();
            this.Transaction.PostedDate = DateTime.Now;
            this.IsPosted = true;

        }

        public void UpdateBalance()
        {

        }

        public ReceivePayment()
        {

        }
    }
}
