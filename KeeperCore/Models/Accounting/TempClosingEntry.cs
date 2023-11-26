using System;
using KeeperCore.ERPNode.Models.ChartOfAccount;

namespace KeeperCore.ERPNode.Models.Accounting
{
    public class TempClosingEntry
    {
        public Guid Id { get; set; }
        public Guid FiscalYearId { get; set; }
        public Guid AccountId { get; set; }
        public AccountItem Account { get; set; }
        public decimal OpeningCredit { get; set; }
        public decimal OpeningDebit { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
}