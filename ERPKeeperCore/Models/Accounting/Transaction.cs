
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
        public PostStatus PostStatus { get; set; }
        public DateTime Date { get; set; }


    }



}
