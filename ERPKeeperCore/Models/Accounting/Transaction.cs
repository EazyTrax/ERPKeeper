﻿
using ERPKeeperCore.Enterprise.Models.Assets;
using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Enterprise.Models.Financial;
using ERPKeeperCore.Enterprise.Models.Taxes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    [Index("FiscalYearId")]
    public partial class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public Enums.TransactionType Type { get; set; }
        public DateTime Date { get; set; }


        public Guid? FiscalYearId { get; set; }
        public String? Name { get; set; }
        public String? ProfileName { get; set; }
        public String? Reference { get; set; }
        public DateTime? PostedDate { get; set; }
        public bool IsPosted => PostedDate != null;

        public Decimal Credit { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Diff => Debit - Credit;

        public virtual ICollection<TransactionLedger> Ledgers { get; set; } = new List<TransactionLedger>();
        public virtual Customers.Sale? Sale { get; set; }
        public virtual Suppliers.Purchase? Purchase { get; set; }
        public virtual Financial.FundTransfer? FundTransfer { get; set; }
        public virtual Customers.ReceivePayment? ReceivePayment { get; set; }
        public virtual Suppliers.SupplierPayment? SupplierPayment { get; set; }
        public virtual Accounting.JournalEntry? JournalEntry { get; set; }
        public virtual Financial.LiabilityPayment? LiabilityPayment { get; set; }
        public virtual Financial.Lend? Lend { get; set; }
        public virtual Financial.LendReturn? LendReturn { get; set; }
        public virtual Financial.Loan? Loan { get; set; }
        public virtual Financial.LoanReturn? LoanReturn { get; set; }
        public virtual Taxes.IncomeTax? IncomeTax { get; set; }
        public virtual Employees.EmployeePayment? EmployeePayment { get; set; }
        public virtual Assets.Asset? Asset { get; set; }
        public virtual Assets.AssetDeprecateSchedule? AssetDeprecateSchedule { get; set; }
        public virtual Taxes.TaxPeriod? TaxPeriod { get; set; }
        public virtual Accounting.FiscalYear? FiscalYearClosing { get; set; }
        public virtual ObsoleteAsset? ObsoleteAsset { get; set; }
        public virtual RetentionPeriod? RetentionGroup { get; internal set; }

        public bool UpdateBalance(decimal? amount = null)
        {
            this.PostedDate = DateTime.Today;

            Debit = Ledgers.Sum(x => x.Debit);
            Credit = Ledgers.Sum(x => x.Credit);

            if (this.Credit != this.Debit || this.Credit == 0)
                Console.WriteLine($"Transaction INFO:> Dr. {this.Debit} Cr.{this.Credit}");

            if (Debit != Credit)
                return false;
            if (amount != null && Debit != amount)
                return false;

            return true;
        }

        public void ClearLedger()
        {
            this.Ledgers.Clear();
            Debit = 0;
            Credit = 0;
            this.PostedDate = null;
        }

        public TransactionLedger AddDebit(Account account, decimal debit = 0, String? memo = null)
        {
            var ledger = new TransactionLedger()
            {
                AccountId = account.Id,
                Debit = debit,
                Credit = 0,
                Date = this.Date,
                Memo = memo,
            };
            this.Ledgers.Add(ledger);
            return ledger;
        }
        public TransactionLedger AddCredit(Account account, decimal credit = 0, String? memo = null)
        {
            var ledger = new TransactionLedger()
            {
                AccountId = account.Id,
                Debit = 0,
                Credit = credit,
                Date = this.Date,
                Memo = memo,
            };
            this.Ledgers.Add(ledger);
            return ledger;
        }
    }
}
