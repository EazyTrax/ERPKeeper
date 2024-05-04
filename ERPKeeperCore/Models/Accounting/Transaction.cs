
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Enums;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public partial class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public String? Reference { get; set; }

        public DateTime? PostedDate { get; set; }
        public bool IsPosted => PostedDate != null;


        public Decimal Credit { get; set; }
        public Decimal Debit { get; set; }


        public virtual ICollection<TransactionLedger> Ledgers { get; set; } = new List<TransactionLedger>();
        public virtual Customers.Sale? Sale { get; set; }
        public virtual Suppliers.Purchase? Purchase { get; set; }
        public virtual Financial.FundTransfer? FundTransfer { get; set; }
        public virtual Customers.ReceivePayment? ReceivePayment { get; set; }
        public virtual Suppliers.SupplierPayment? SupplierPayment { get; set; }
        public virtual Accounting.JournalEntry? JournalEntry { get; set; }
        public Guid? FiscalYearId { get; set; }

        public void UpdateBalance()
        {
            Debit = Ledgers.Sum(x => x.Debit);
            Credit = Ledgers.Sum(x => x.Credit);
        }

        public void ClearLedger()
        {
            this.Ledgers.Clear();
            this.PostedDate = null;
        }

        public void AddDebit(Account account, decimal debit = 0)
        {
            var ledger = new TransactionLedger()
            {
                AccountId = account.Id,
                Debit = debit,
                Credit = 0,
            };
            this.Ledgers.Add(ledger);
        }
        public void AddCredit(Account account, decimal credit = 0)
        {
            var ledger = new TransactionLedger()
            {
                AccountId = account.Id,
                Debit = 0,
                Credit = credit,
            };
            this.Ledgers.Add(ledger);
        }


    }


    public partial class TransactionLedger
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
