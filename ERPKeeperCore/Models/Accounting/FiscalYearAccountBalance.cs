using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public class FiscalYearAccountBalance
    {
        public Guid Id { get; set; }
        public Guid FiscalYearId { get; set; }
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
        public Decimal ClosingDebit { get; set; }
        public Decimal ClosingCredit { get; set; }
        public Decimal ClosedDebit { get; set; }
        public Decimal ClosedCredit { get; set; }
    }
}