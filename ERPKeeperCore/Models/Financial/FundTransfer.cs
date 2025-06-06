using ERPKeeperCore.Enterprise.Models.Financial.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Financial
{

    public class FundTransfer
    {
        [Key]
        public Guid Id { get; set; }


        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }


        public DateTime Date { get; set; }
        public String? Reference { get; set; }
        public FundTransferStatus Status { get; set; }
        public string Name => string.Format("{0}-{1}/{2}", "FT", DocumentGroup, No.ToString().PadLeft(3, '0'));
        public string DocumentGroup => this.Date.ToString("yyMM");
        public int No { get; set; }
        public String? Memo { get; set; }

        public Guid? WithDrawAccountId { get; set; }
        [ForeignKey("WithDrawAccountId")]
        public virtual Accounting.Account WithDrawAccount { get; set; }

        public Decimal WithDrawAmount { get; set; }

        public Guid? BankFee_Expense_AccountId { get; set; }
        [ForeignKey("BankFee_Expense_AccountId")]
        public virtual Accounting.Account BankFee_Expense_Account { get; set; }

        public Decimal BankFeeAmount { get; set; }
        public Guid? Deposit_Asset_AccountId { get; set; }
        [ForeignKey("Deposit_Asset_AccountId")]
        public virtual Accounting.Account Deposit_Asset_Account { get; set; }

        public Decimal DepositAmount => WithDrawAmount - BankFeeAmount;

        public void UpdateBalance()
        {
           
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post  FT:{this.Name}");

            if (this.Transaction == null) return;
            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Today;


            //Step 2. Prepare Data
            this.UpdateBalance();

            //Step 3. Post Data
            // Dr.
            this.Transaction.AddDebit(this.Deposit_Asset_Account, this.DepositAmount);
            if (this.BankFeeAmount > 0)
                this.Transaction.AddDebit(this.BankFee_Expense_Account, this.BankFeeAmount);
            // Cr.
            this.Transaction.AddCredit(this.WithDrawAccount, this.WithDrawAmount);


            // Step 4 Finalize
            IsPosted = this.Transaction.UpdateBalance();

        }

        public void UpdateName()
        {
        }
    }
}