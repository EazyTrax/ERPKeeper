using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPKeeper.Node.Models.Accounting;
using System.ComponentModel;

namespace ERPKeeper.Node.Models.Transactions.Commercials
{
    public class Purchase : Commercial
    {
  

        public Purchase()
        {
            this.Uid = Guid.NewGuid();
            this.TrnDate = DateTime.Now;
            this.Status = CommercialStatus.Open;
            this.TransactionType = Accounting.Enums.ERPObjectType.Purchase;
        }
    

    
    }
}
