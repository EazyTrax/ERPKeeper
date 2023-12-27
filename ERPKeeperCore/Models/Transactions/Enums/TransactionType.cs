using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Transactions
{
    public enum TransactionType
    {
        All = 0,
        Lastest100 = 20,
        Status = 30,
        Time = 40,
        Overdue = 50
    }
}

