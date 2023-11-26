
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.AccountingEntries
{

    [Table("ERP_Journal_JournalEntryItem")]
    public class JournalEntryItem
    {
        [Key]
        [Column("Id")]
        public Guid JournalEntryItemId { get; set; }

        public Guid JournalEntryId { get; set; }

        [ForeignKey("JournalEntryId")]
        public virtual Models.AccountingEntries.JournalEntry JournalEntry { get; set; }


        [Column("AccountId")]
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual ChartOfAccount.AccountItem Account { get; set; }
        public Decimal? Debit { get; set; }
        public Decimal? Credit { get; set; }

        public String Memo { get; set; }

        public JournalEntryItem()
        {
 
        }

        public void Update(JournalEntryItem item)
        {
            this.AccountId = item.AccountId;
            this.Debit = item.Debit;
            this.Credit = item.Credit;
            this.Memo = item.Memo;
            this.JournalEntry.UpdateAmount();
        }
    }
}