using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KeeperCore.ERPNode.Models.ChartOfAccount;

namespace KeeperCore.ERPNode.Models.Transactions.Commercials
{
    public class Sale : Commercial
    {

        public Sale()
        {
            
            this.TrnDate = DateTime.Now;
            this.Status = CommercialStatus.Open;
            this.TrnDate = DateTime.Now;
            this.TransactionType = Accounting.Enums.ERPObjectType.Sale;
        }

    }
}
