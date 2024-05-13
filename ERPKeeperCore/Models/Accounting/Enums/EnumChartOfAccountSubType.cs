using System.ComponentModel.DataAnnotations;

namespace ERPKeeperCore.Enterprise.Models.Accounting.Enums
{
    public enum AccountSubTypes
    {
        None = 0,

        /// <summary>
        /// Asset Types
        /// </summary>
        /// 
        Asset_Cash = 100,
        Asset_Bank = 120,
        ShotTermLending = 125,
        LongTermLending = 126,
        ShotTermInvestment = 130,
        OtherCurrentAsset = 140,
        Inventory = 160,
        OtherAsset = 180,
        AccountReceivable = 170,
        FixedAsset = 190,
        AccDepreciation = 198,
        EarnestPayment = 191,
        TaxInput = 181,
        TaxReceivable = 183,


        /// <summary>
        /// Liability Type
        /// </summary>
        PayrollLiability = 220,
        CurrentLiability = 240,
        LongTermLiability = 260,
        AccountPayable = 270,
        TaxOutput = 281,
        TaxPayable = 282,


        EarnestReceive = 291,
        OverReceivePayment = 292,
        UnpaidCheck = 295,
        PendingItemReceipts = 298,

        /// <summary>
        /// Equity Type
        /// </summary>
        Equity = 300,
        Equity_Stock = 301,
        Equity_RetainEarning = 302,
        Equity_OverStockValue = 303,
        Equity_OpeningBalance = 304,
        Equity_Dividend = 305,


        /// <summary>
        /// Income
        /// </summary>
        Income = 400,
        Interest = 402,
        OtherIncome = 450,
        DiscountTaken = 452,



        /// <summary>
        /// Expense Type
        /// </summary>
        Expense = 500,
        OtherExpense = 550,
        Expense_CostOfGoodsSold = 570,
        Expense_Payroll = 580,
        Expense_DiscountGiven = 555,
        Expense_BankFee = 556,
        Expense_TaxExpense = 560,
        Expense_IncomeTax = 563,
        Expense_PurchaseAsset = 564,
        IncomeTaxExpense = 562,
        AccumulateExpense = 561,
        AmortizeExpense = 582,
        AwaitingDepreciation = 583,
        InvestmentRevenue = 584,
        UnCliamableExpense = 585,

    }
}
