using KeeperCore.ERPNode.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeeperCore.ERPNode.Models
{
    public class FiscalYearAccountBalance
    {
        public Guid Id { get; set; }
        public Guid FiscalYearId { get; set; }
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public decimal Debit { get; set; }       
        public decimal Credit { get; set; }

        public decimal Balance
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

        public decimal OpeningDebit { get; internal set; }
        public decimal OpeningCredit { get; internal set; }
        public decimal ClosingDebit { get; internal set; }
        public decimal ClosingCredit { get; internal set; }
    }
}