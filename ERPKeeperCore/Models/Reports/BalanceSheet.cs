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
        public List<Account> AssetAccounts { get; set; }
        public List<Account> LiabilityAccounts { get; set; }
        public List<Account> EquityAccounts { get; set; }
    }
}
