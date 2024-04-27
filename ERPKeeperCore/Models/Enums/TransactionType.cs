using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Transactions
{
    public enum TransactionType
    {
        UnKnow = 0,
        Sale = 20,
        Purchase = 30,
        Payment = 40,
        ReceivePayment = 50,
    }
}

