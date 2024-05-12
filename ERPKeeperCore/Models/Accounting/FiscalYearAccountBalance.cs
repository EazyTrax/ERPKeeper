using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public class FiscalYearAccountBalance
    {
        public Guid Id { get; set; }

        public Guid FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual FiscalYear FiscalYear { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }




        public Decimal OpeningDebit { get; set; }
        public Decimal OpeningCredit { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }
        public Decimal ClosingDebit { get; set; }
        public Decimal ClosingCredit { get; set; }





       
        public Decimal ClosedDebit
        {
            get
            {
                var curDebit = OpeningDebit + Debit + ClosingDebit;
                var curCredit = OpeningCredit + Credit + ClosingCredit;
                return Math.Max(curDebit - curCredit, 0);
            }
        }
        public Decimal ClosedCredit
        {
            get
            {
                var curDebit = OpeningDebit + Debit + ClosingDebit;
                var curCredit = OpeningCredit + Credit + ClosingCredit;
                return Math.Max(curCredit - curDebit, 0);
            }
        }

        public void ClearBalance()
        {
            this.OpeningDebit = 0;
            this.OpeningCredit = 0;
            this.Debit = 0;
            this.Credit = 0;
        }
    }
}