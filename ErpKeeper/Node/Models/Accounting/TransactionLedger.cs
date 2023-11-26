
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Accounting
{
    [Table("ERP_Ledger_Transactions")]
    public class TransactionLedger
    {
        [Key]
        public Guid Uid { get; set; }
        public Enums.ERPObjectType TransactionType { get; set; }
        [Index]
        public Guid? TransactionGuid { get; set; }


        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }

        public Guid? FiscalYearUid { get; set; }
        public string TransactionName { get; set; }
        public int TransactionNo { get; set; }
        public String Name => TransactionName;

        public String Reference { get; set; }
        public String ReportDisplayName => string.Format("{0} {1} · {2}{4}{3}", this.TransactionType.ToString(), this.TransactionName, this.Reference, this.ProfileName, Environment.NewLine);

        public string ProfileName { get; set; }
        public Guid? ProfileGuid { get; set; }

        public Decimal? BaseAmount { get; set; }

        [MaxLength(500)]
        public String Memo { get; set; }

        [Column(TypeName = "Money")]
        [DefaultValue(0)]
        public Decimal TotalDebit { get; set; }

        [Column(TypeName = "Money")]
        [DefaultValue(0)]
        public Decimal TotalCredit { get; set; }




        public virtual ICollection<Ledger> Ledgers { get; set; }

        public virtual ICollection<Ledger> DrLedgers() => this.Ledgers.Where(l => l.Debit != null).ToList();
        public virtual ICollection<Ledger> CrLedgers() => this.Ledgers.Where(l => l.Credit != null).ToList();


        public TransactionLedger()
        {
            this.Uid = Guid.NewGuid();
        }

        public void Debit(AccountItem AccountItem, decimal amount, string memo = "")
        {
            if (amount == 0) return;
            if (AccountItem == null) return;

            Models.Accounting.Ledger ledger = new Models.Accounting.Ledger()
            {
                TransactionGuid = this.Uid,
                TrnDate = this.TrnDate,
                TransactionName = this.TransactionName,
                TransactionType = this.TransactionType,
                Debit = Math.Round(amount, 2, MidpointRounding.ToEven),
                Credit = null,
                AccountUid = AccountItem.Uid,
                AccountName = AccountItem.Name,
                accountItem = AccountItem,
                Memo = memo,
                ledgerPostType = LedgerPostType.Debit,

            };

            if (this.Ledgers == null)
                this.Ledgers = new HashSet<Ledger>();

            this.Ledgers.Add(ledger);
        }

        public void Credit(AccountItem AccountItem, decimal amount, string memo = "")
        {
            if (amount == 0) return;
            if (AccountItem == null) return;

            Models.Accounting.Ledger ledger = new Models.Accounting.Ledger()
            {
                TransactionGuid = this.Uid,
                TrnDate = this.TrnDate,
                TransactionName = this.TransactionName,
                TransactionType = this.TransactionType,
                Debit = null,
                Credit = Math.Round( amount,2, MidpointRounding.ToEven),
                AccountUid = AccountItem.Uid,
                AccountName = AccountItem.Name,
                accountItem = AccountItem,
                Memo = memo,
                ledgerPostType = LedgerPostType.Credit,
            };

            if (this.Ledgers == null)
                this.Ledgers = new HashSet<Ledger>();

            this.Ledgers.Add(ledger);
        }

        public void RemoveLedgers()
        {
            if (this.Ledgers == null)
                return;

            var clearLedgers = this.Ledgers.ToList();
            clearLedgers.ForEach(ledger =>
            {
                this.Ledgers.Remove(ledger);
            });

            TotalDebit = 0;
            TotalCredit = 0;
        }

        public bool FinalValidate(bool AcknowledgeRequried = false)
        {
            TotalDebit = this.Ledgers?.Sum(l => l.Debit) ?? 0;
            TotalCredit = this.Ledgers?.Sum(l => l.Credit) ?? 0;

            if (TotalDebit - TotalCredit <= (Decimal)0.1)
                return true;
            else
            {
                Console.WriteLine($"[ERROR] {TransactionType}-{Name} fail posting, debit {TotalDebit} != credit {TotalCredit}");

                this.Ledgers.ToList().OrderBy(l => l.Debit).ThenBy(l => l.Credit).ToList().ForEach(l =>
                    {
                        Console.WriteLine($"{l.accountItem.Code}\t\tDr.{l.Debit ?? 0}\t\tCr.{l.Credit ?? 0}");
                    });

                this.RemoveLedgers();
                return false;
            }
        }
    }
}