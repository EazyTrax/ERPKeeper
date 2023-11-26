using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Accounting.Enums
{
    public enum ERPObjectType
    {
        NotSet = 0,
        Sale = 30,
        Purchase = 60,
        FixedAsset = 95,
        FixedAssetRetirement = 96,
        EmployeePayment = 110,
        CapitalActivity = 120,
        OpeningEntry = 125,
        FiscalYearClosing = 130,
        FundTransfer = 131,
        PaySalesTax = 135,
        OpeningInventoryItem = 145,
        OpeningFixedAsset = 150,
        Loan = 160,
        LoanPayment = 161,
        Lend = 170,
        LendPayment = 162,
        JournalEntry = 190,
        TaxPeriod = 200,
        IncomeTax = 210,
        EarnestPayment = 220,
        EarnestReceive = 230,
        FixedAssetUsage = 232,
        InputTax = 233,
        OutputTax = 243,
        Expense = 244,
        GeneralPayment = 240,
        SupplierPayment = 245,
        LiabilityPayment = 246,
        ReceivePayment = 248,
        CommercialPayment = 249,
        SalesReturn = 250,
        RetentionPayment = 251,
        Asset = 252,
        PurchaseReturn = 254,
        ItemCOGS = 255,
        DeprecateSchedule = 256,
        FIFOCost = 257,
        CommercialTax = 258,
        SaleQuote = 259,
        PurchaseQuote = 260,
        TaxPayment = 261,
        Customer = 262,
        Supplier = 263,
        Employee = 264,
        Investor = 265,
        Item = 266,
        RetentionGroup = 267
    }
}