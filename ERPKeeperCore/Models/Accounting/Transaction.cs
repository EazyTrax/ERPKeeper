﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Transactions;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public partial class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }       
        public DateTime Date { get; set; }


        public Decimal TotalCredit { get; set; }
        public Decimal TotalDebit { get; set; }

        public virtual ICollection<TransactionLedger> Ledgers { get; set; } = new HashSet<TransactionLedger>();

    }

    public partial class TransactionLedger
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual required Transaction Transaction { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual required Account Account { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}