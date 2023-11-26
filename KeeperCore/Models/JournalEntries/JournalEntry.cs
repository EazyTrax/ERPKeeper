
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Transactions;

namespace KeeperCore.ERPNode.Models.AccountingEntries
{

    [Table("ERP_JournalEntry")]
    public class JournalEntry
    {
        [Key]
        
        public Guid Id { get; set; }
        public int No { get; set; }
        public string Name => string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));

        public const Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.JournalEntry;
        
        public DateTime TrnDate { get; set; }
        public Guid? JournalEntryTypeId { get; set; }
        [ForeignKey("JournalEntryTypeId")]
        public virtual JournalEntryType JournalEntryType { get; set; }
        public String Memo { get; set; }

        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }

        public virtual ICollection<JournalEntryItem> Items { get; set; }

        public LedgerPostStatus PostStatus { get; set; }

        public DateTime? JournalPostDate { get; set; }
        public CommercialStatus Status { get; set; }

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual FiscalYear FiscalYear { get; set; }

        public String FiscalYearName => FiscalYear?.Name;

        public JournalEntry()
        {
            
        }


        public virtual void UpdateAmount()
        {
            Debit = (Items?.Select(i => i.Debit).DefaultIfEmpty(0).Sum()) ?? 0;
            Credit = (Items?.Select(i => i.Credit).DefaultIfEmpty(0).Sum()) ?? 0;
        }

        public void AddAcount(Guid accountId)
        {
            var jourmalEntryItem = new JournalEntryItem()
            {
                JournalEntryItemId = Guid.NewGuid(),
                AccountId = accountId
            };

            this.Items.Add(jourmalEntryItem);
            this.UpdateAmount();
        }


    }
}