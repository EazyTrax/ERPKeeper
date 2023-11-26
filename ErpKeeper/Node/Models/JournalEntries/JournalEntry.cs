
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Transactions;

namespace ERPKeeper.Node.Models.Accounting
{

    [Table("ERP_JournalEntry")]
    public class JournalEntry
    {
        [Key]
        [Column("GID")]
        public Guid Uid { get; set; }
        public int No { get; set; }
        public string Name => string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));

        public const Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.JournalEntry;
        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }
        public Guid? JournalEntryTypeGuid { get; set; }
        [ForeignKey("JournalEntryTypeGuid")]
        public virtual JournalEntryType JournalEntryType { get; set; }
        public String Memo { get; set; }

        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }

        public virtual ICollection<JournalEntryItem> Items { get; set; }

        public LedgerPostStatus PostStatus { get; set; }

        public DateTime? JournalPostDate { get; set; }
        public CommercialStatus Status { get; set; }

        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public FiscalYear FiscalYear { get; set; }

        public String FiscalYearName => FiscalYear?.Name;

        public JournalEntry()
        {
            this.Uid = Guid.NewGuid();
        }


        public virtual void UpdateBalance()
        {
            Debit = (Items?.Select(i => i.Debit).DefaultIfEmpty(0).Sum()) ?? 0;
            Credit = (Items?.Select(i => i.Credit).DefaultIfEmpty(0).Sum()) ?? 0;
        }

        public void AddAcount(Guid accountUid, decimal? debit = 0, decimal? credit = 0)
        {
            var jourmalEntryItem = new JournalEntryItem()
            {
                JournalEntryItemId = Guid.NewGuid(),
                AccountUid = accountUid,
                Debit = debit,
                Credit = credit
            };

            this.Items.Add(jourmalEntryItem);
            this.UpdateBalance();
        }


    }
}