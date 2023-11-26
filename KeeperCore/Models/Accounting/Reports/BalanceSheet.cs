using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperCore.ERPNode.Models.Accounting.Reports
{
    public class BalanceSheet
    {
        public DateTime TrnDate { get; set; }
        public List<AccountItem> AssetAccounts { get; set; }
        public List<AccountItem> LiabilityAccounts { get; set; }
        public List<AccountItem> EquityAccounts { get; set; }
    }
}
