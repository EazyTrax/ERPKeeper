﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Transactions
{
    public enum CommericalTransactionStatus
    {
        Open = 0,
        PartialPaid = 1,
        Paid = 2,
        Void = 99
    }

    public enum CommercialViewStatus
    {
        Open = 0,
        PartialPaid = 1,
        Paid = 2,
        All = 3,
        OverDue = 4,
        LastFiscal = 98,
        Void = 99
    }
}

