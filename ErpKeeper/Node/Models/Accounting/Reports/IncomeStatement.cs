using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeper.Node.Models.Accounting.Reports
{
    public class IncomeStatement
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AccountItem> Incomes { get; set; }
        public List<AccountItem> Expenses { get; set; }
        public Decimal Profit { get; set; }
    }
}
