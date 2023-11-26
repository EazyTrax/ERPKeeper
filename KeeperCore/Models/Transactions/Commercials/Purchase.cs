using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System.ComponentModel;

namespace KeeperCore.ERPNode.Models.Transactions.Commercials
{
    public class Purchase : Commercial
    {
        public Purchase()
        {
            
            this.Status = CommercialStatus.Open;
            this.TransactionType = Accounting.Enums.ERPObjectType.Purchase;
        }
    }
}
