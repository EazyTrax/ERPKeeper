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
        Asset_ShotTermLending = 125,
        Asset_LongTermLending = 126,
        Asset_ShotTermInvestment = 130,
        Asset_OtherCurrent = 140,
        Asset_Inventory = 160,
        Asset_OtherAsset = 180,
        Asset_AccountReceivable = 170,

        Asset_TaxInput = 181,
        Asset_TaxReceivable = 183,
        Asset_Shot_Asset_Purchase = 564,

        Asset_Long_FixedAsset = 190,
        Asset_Long_WorkingAsset = 191,
        Asset_AwaitingDepreciation = 583,
        Asset_AccDepreciation = 198,
        Asset_EarnestPayment = 191,
    


        /// <summary>
        /// Liability Type
        /// </summary>
        Liability_Payroll = 220,
        Liability_Current = 240,
        Liability_LongTerm = 260,
        Liability_AccountPayable = 270,
        Liability_TaxOutput = 281,
        Liability_TaxPayable = 282,


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
        Equity_RetainEarning_Reserve = 306,

        /// <summary>
        /// Income
        /// </summary>
        Income = 400,
        Income_Interest = 402,
        Income_Other = 450,
        Income_DiscountTaken = 452,
        Income_InvestmentRevenue = 584,


        /// <summary>
        /// Expense Type
        /// </summary>
        Expense = 500,
        Expense_Administrative = 510,
        Expense_Selling = 520,
        Expense_Other = 550,
        Expense_Interest = 552,
        Expense_CostOfGoodsSold = 570,
        Expense_Payroll = 580,
        Expense_DiscountGiven = 555,
        Expense_BankFee = 556,
        Expense_Prohibit = 560,
        Expense_IncomeTax = 563,

        Expense_Asset_Deprecate = 561,
        Expense_Asset_Amortize = 582,
        Expense_NonRefundableTax = 585,

    }
}
