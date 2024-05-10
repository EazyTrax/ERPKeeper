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
using ERPKeeperCore.Enterprise.Models.Accounting;


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

        public Decimal Amount { get; set; }
        public Decimal AmountExcludeTax { get; set; }

        public Decimal AmountRetention { get; set; }
        public Decimal AmountDiscount { get; set; }
        public Decimal AmountTotal => Amount - (AmountRetention + AmountDiscount);

        public Decimal AmountBankFee { get; set; }


        //Cr.
        public Guid? PayFrom_AssetAccountId { get; set; }
        [ForeignKey("PayFrom_AssetAccountId")]
        public virtual Accounting.Account? AssetAccount_PayFrom { get; set; }


        // Dr.
        public Guid? LiablityAccount_SupplierPayableId { get; set; }
        [ForeignKey("LiablityAccount_SupplierPayableId")]
        public virtual Accounting.Account? LiablityAccount_SupplierPayable { get; set; }


        public Guid? RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual Financial.RetentionType? RetentionType { get; set; }

        public Guid? IncomeAccount_DiscountTakenId { get; set; }
        [ForeignKey("IncomeAccount_DiscountTakenId")]
        public virtual Account? IncomeAccount_DiscountTaken { get; set; }


        public Guid? ExpenseAccount_BankFeeId { get; set; }
        [ForeignKey("ExpenseAccount_BankFeeId")]
        public virtual Account? ExpenseAccount_BankFee { get; set; }


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
        }


        public void PostToTransaction()
        {
            Console.WriteLine($">Post SupplierPayments:{this.Name} From {this.Purchase.Name}");

            if (this.Transaction == null)
                return;
            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Now;

            // Dr. 
            this.Transaction.AddDebit(this.LiablityAccount_SupplierPayable, this.Amount);

            // Cr.
            if (this.AmountDiscount > 0)
                this.Transaction.AddCredit(this.IncomeAccount_DiscountTaken, this.AmountDiscount);
            if (this.RetentionTypeId != null)
                this.Transaction.AddCredit(this.RetentionType.RetentionTo_LiabilityAccount, this.AmountRetention);
            this.Transaction.AddCredit(this.AssetAccount_PayFrom, this.AmountTotal);


            if (this.AmountBankFee > 0)
            {
                //Dr.
                this.Transaction.AddDebit(this.ExpenseAccount_BankFee, this.AmountBankFee);
                //Cr.
                this.Transaction.AddCredit(this.AssetAccount_PayFrom, this.AmountBankFee);
            }



            this.Transaction.UpdateBalance();
            this.IsPosted = true;
        }

    }
}
