using ERPKeeperCore.Enterprise.Models.Financial.Enums;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Financial
{

    public class FundTransferItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FundTransferId { get; set; }
        [ForeignKey("FundTransferId")]
        public virtual FundTransfer FundTransfer { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Accounting.Account Account { get; set; }

        public Decimal Debit { get; set; }
        public Decimal Credit{ get; set; }

        public String? Memo { get; set; }




    }
}