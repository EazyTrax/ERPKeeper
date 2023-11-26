
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Accounting
{

    [Table("ERP_Journal_JournalEntryItem")]
    public class JournalEntryItem
    {
        [Key]
        [Column("Uid")]
        public Guid JournalEntryItemId { get; set; }

        public Guid JournalEntryId { get; set; }

        [ForeignKey("JournalEntryId")]
        public virtual Models.Accounting.JournalEntry JournalEntry { get; set; }


        [Column("AccountUid")]
        public Guid AccountUid { get; set; }
        [ForeignKey("AccountUid")]
        public virtual Accounting.AccountItem Account { get; set; }
        public Decimal? Debit { get; set; }
        public Decimal? Credit { get; set; }

        public String Memo { get; set; }

        public JournalEntryItem()
        {
            this.JournalEntryItemId = Guid.NewGuid();
        }

        public void Update(JournalEntryItem item)
        {
            this.AccountUid = item.AccountUid;
            this.Debit = item.Debit;
            this.Credit = item.Credit;
            this.Memo = item.Memo;
            this.JournalEntry.UpdateBalance();
        }
    }
}