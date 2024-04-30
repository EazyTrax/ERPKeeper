using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Accounting.Enums
{
    public enum TransactionType
    {
        UnKnow = 0,
        Sale = 20,
        Purchase = 30,
        DebtPayment = 40,
        ReceivePayment = 50,
        FundTransfer = 60,
    }
}

