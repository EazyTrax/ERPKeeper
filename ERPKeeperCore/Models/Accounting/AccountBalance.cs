using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public class AccountBalance
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public DateTime Date { get; set; }

        public Decimal Credit { get; set; } = 0;
        public Decimal Debit { get; set; } = 0;
        public Decimal TotalCredit { get; set; } = 0;
        public Decimal TotalDebit { get; set; } = 0;


    }
}