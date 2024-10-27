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
        SupplierPayment = 70,
        JournalEntry = 80,
        LiabilityPayment = 90,
        Lend = 100,
        LendReturn = 101,

        Loan = 110,
        LoanReturn = 112,
        IncomeTax = 130,
        TaxPeriod = 131,
        EmployeePayment = 150,
        Asset = 160,
        AssetDeprecateSchedule = 161,
        FiscalYearClosing = 200,
        ObsoleteAsset = 201,
        RetentionGroup = 202,
    }
}

