using System;
using ERPKeeper.Node.Models.Accounting;

namespace ERPKeeper.Node.Models.Accounting
{
    public class TempClosingEntry
    {
        public Guid Uid { get; set; }
        public Guid FiscalYearUid { get; set; }
        public Guid AccountGuid { get; set; }
        public AccountItem Account { get; set; }



        public decimal OpeningCredit { get; set; }
        public decimal OpeningDebit { get; set; }






        public decimal Credit { get; set; }
        public decimal Debit { get; set; }




    }
}