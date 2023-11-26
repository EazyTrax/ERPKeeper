using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace ERPKeeper.Node.Models.Accounting.FiscalYears
{
    [Table("ERP_FiscalYear_ClosingAccounts")]
    public class PeriodAccountBalance
    {
        public PeriodAccountBalance()
        {

        }

        [Key]
        public Guid Uid { get; set; }
        public Guid? AccountGuid { get; set; }
        [ForeignKey("AccountGuid")]
        public virtual AccountItem Account { get; set; }

        public LedgerPostStatus PostStatus { get; set; }
        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public virtual FiscalYear FiscalYear { get; set; }

        public decimal OpeningCredit { get; set; }
        public decimal OpeningDebit { get; set; }

        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }

        public Decimal ClosingDebit { get; set; }
        public Decimal ClosingCredit { get; set; }

        public Decimal ClosedDebit => Math.Max((OpeningDebit + Debit + ClosingDebit) - (OpeningCredit + Credit + ClosingCredit), 0);
        public Decimal ClosedCredit => Math.Max((OpeningCredit + Credit + ClosingCredit) - (OpeningDebit + Debit + ClosingDebit), 0);

        [Column(TypeName = "Money")]
        [DisplayFormat(DataFormatString = "N2")]
        public virtual Decimal CurrentBalance
        {
            get
            {
                switch (this.Account.Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return (this.Debit) - (this.Credit);

                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                        return (this.Credit) - (this.Debit);
                    default:
                        return 0;
                }
            }
        }


        [Column(TypeName = "Money")]
        [DisplayFormat(DataFormatString = "N2")]
        public virtual Decimal Balance
        {
            get
            {
                switch (this.Account.Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return (this.ClosedDebit) - (this.ClosedCredit);

                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                        return (this.ClosedCredit) - (this.ClosedDebit);
                    default:
                        return 0;
                }
            }
        }

        internal void resetBalance()
        {
            OpeningDebit = 0;
            Debit = 0;
            ClosingDebit = 0;

            OpeningCredit = 0;
            Credit = 0;
            ClosingCredit = 0;
        }
    }
}
