
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
    public enum LedgerPostType
    {
        Debit = 0,
        Credit = 1
    }


    [Table("ERP_Ledgers")]
    public class Ledger
    {
        [Key]
        public Guid Uid { get; set; }

        public Enums.ERPObjectType TransactionType { get; set; }

        public Guid TransactionLedgerUid { get; set; }
        [ForeignKey("TransactionLedgerUid")]
        public virtual TransactionLedger TransactionLedger { get; set; }

        public LedgerPostType ledgerPostType { get; set; }


        public int Index { get; set; }

        [Index]
        public Guid? TransactionGuid { get; set; }

        [Column("TrnDate")]

        public DateTime TrnDate { get; set; }
        public int TransactionNo { get; set; }
        public string TransactionName { get; set; }
        public String Name => TransactionName;

        public string ProfileName { get; set; }
        public Guid? ProfileGuid { get; set; }

        public Guid? FiscalYearUid { get; set; }


        public int OrderNo { get; set; }

        [Column("AccountName")]
        public String AccountName { get; set; }
        public Guid AccountUid { get; set; }
        [ForeignKey("AccountUid")]
        public virtual AccountItem accountItem { get; set; }

        public String ReportDisplayAccountName
        {
            get
            {
                if (this.ledgerPostType == LedgerPostType.Credit)
                    return "    " + accountItem?.Name ?? "NULL Account";
                else
                    return accountItem?.Name ?? "NULL Account";
            }
        }

        [MaxLength(500)]
        public String Memo { get; set; }

        [Column(TypeName = "Money")]
        [DefaultValue(0)]
        public Decimal? Debit { get; set; }

        [Column(TypeName = "Money")]
        [DefaultValue(0)]
        public Decimal? Credit { get; set; }


        [Column(TypeName = "Money")]
        [DisplayFormat(DataFormatString = "N2")]
        public virtual Decimal Balance
        {
            get
            {
                switch (this.accountItem.Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return (Debit ?? 0) - (Credit ?? 0);

                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                        return (Credit ?? 0) - (Debit ?? 0);
                    default:
                        return 0;
                }
            }
        }

        [DisplayFormat(DataFormatString = "N2")]
        [DefaultValue(0)]
        public virtual Decimal TotalBalance
        {
            get; set;
        }

        public Ledger()
        {
            this.Uid = Guid.NewGuid();
        }
    }
}