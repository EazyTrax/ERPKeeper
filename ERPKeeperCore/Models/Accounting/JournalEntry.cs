
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace ERPKeeperCore.Enterprise.Models.Accounting
{


    public class JournalEntry
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }






        public int No { get; set; }
        public String? Name => string.Format("{0}/{1}", this.Date.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));


        public DateTime Date { get; set; }

        public Guid? JournalEntryTypeId { get; set; }
        [ForeignKey("JournalEntryTypeId")]

        public virtual JournalEntryType JournalEntryType { get; set; }
        public String? Memo { get; set; }

        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }

        public virtual ICollection<JournalEntryItem> JournalEntryItems { get; set; } = new List<JournalEntryItem>();
        public String? Description { get; set; }

        public JournalEntry()
        {

        }


        public virtual void UpdateBalance()
        {
            Debit = (JournalEntryItems?.Select(i => i.Debit).DefaultIfEmpty(0).Sum()) ?? 0;
            Credit = (JournalEntryItems?.Select(i => i.Credit).DefaultIfEmpty(0).Sum()) ?? 0;
        }

        public void AddAcount(Guid accountUid, decimal? debit, decimal? credit)
        {
            debit = debit ?? 0;
            credit = credit ?? 0;

            var jourmalEntryItem = new JournalEntryItem()
            {
                AccountId = accountUid,
                Debit = debit,
                Credit = credit
            };

            this.JournalEntryItems.Add(jourmalEntryItem);
            this.UpdateBalance();
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post  JourmalEntry:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();


            foreach (var item in this.JournalEntryItems)
            {
                Console.WriteLine($"{item.Account.Name} Dr.{item.Debit}  Cr.{item.Credit} ");

                if ((item.Debit ?? 0) > 0)
                    this.Transaction.AddDebit(item.Account, item.Debit ?? 0);
                if ((item.Credit ?? 0) > 0)
                    this.Transaction.AddCredit(item.Account, item.Credit ?? 0);
            }



            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Name;
            this.Transaction.UpdateBalance();
            this.Transaction.PostedDate = DateTime.Now;
            this.IsPosted = true;

        }


        public void RemoveAllItems()
        {
            this.JournalEntryItems.Clear();
        }

    }
}