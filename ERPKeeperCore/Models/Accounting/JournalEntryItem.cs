
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{


    public class JournalEntryItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid JournalEntryId { get; set; }
        [ForeignKey("JournalEntryId")]
        public virtual Models.Accounting.JournalEntry JournalEntry { get; set; }


        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Accounting.Account Account { get; set; }

        public Decimal? Debit { get; set; }
        public Decimal? Credit { get; set; }

        public String? Memo { get; set; }

        public JournalEntryItem()
        {

        }

        public void Update(JournalEntryItem item)
        {
            this.AccountId = item.AccountId;
            this.Debit = item.Debit;
            this.Credit = item.Credit;
            this.Memo = item.Memo;
            this.JournalEntry.UpdateBalance();
        }
    }
}