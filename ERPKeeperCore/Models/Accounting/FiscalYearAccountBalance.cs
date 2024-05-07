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


        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }

        public Decimal Balance
        {
            get
            {
                switch (Account.Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return Debit - Credit;

                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                    default:
                        return Credit - Debit;
                }
            }
        }


        public Decimal OpeningDebit { get; set; }
        public Decimal OpeningCredit { get; set; }

        public Decimal ClosingDebit
        {
            get
            {
                if (this.Account.Type == AccountTypes.Income || this.Account.Type == AccountTypes.Expense)
                    return this.Credit;
                return 0;
                //if (this.Account.Type == AccountTypes.Capital && this.Account.SubType == AccountSubTypes.Equity_RetainEarning)
                //{
                //    var totalDebit = this.FiscalYear.FiscalYearAccountBalances
                //        .Where(x=> x.Account.Type == AccountTypes.Income || x.Account.Type == AccountTypes.Expense)
                //        .ToList()
                //        .Sum(x => x.Debit);

                //    var totalCredit = this.FiscalYear.FiscalYearAccountBalances
                //        .Where(x => x.Account.Type == AccountTypes.Income || x.Account.Type == AccountTypes.Expense)
                //        .ToList()
                //        .Sum(x => x.Credit);
                //}
            }
        }
        public Decimal ClosingCredit
        {
            get
            {
                if (this.Account.Type == AccountTypes.Income || this.Account.Type == AccountTypes.Expense)
                    return this.Debit;
                return 0;
            }
        }

        public Decimal ClosedDebit { get; set; }
        public Decimal ClosedCredit { get; set; }

        public void ClearBalance()
        {
            this.OpeningDebit = 0;
            this.OpeningCredit = 0;
            this.Debit = 0;
            this.Credit = 0;
            this.ClosedDebit = 0;
            this.ClosedCredit = 0;
        }
    }
}