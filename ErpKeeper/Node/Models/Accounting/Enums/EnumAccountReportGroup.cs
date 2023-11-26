namespace ERPKeeper.Node.Models.Accounting
{
    public enum AccountReportGroup
    {

        Asset_Current = 110,
        Asset_Current_CashEquvalent = 111,
        Asset_Current_Inventory = 112,
        Asset_Current_Other = 114,

        Asset_NonCurrent =120,
        Asset_NonCurrent_Asset = 121,
        Asset_NotCurrent_LongTermLending = 122,
        Asset_NotCurrent_Other = 124,


        Liability_Current = 210,
        Liability_NonCurrent = 220,

        Equity_Stock = 310,
        Equity_RetainEarning = 320,

        Income = 400,

        Expense_Operation = 510,
        Expense_SaleAndMarketing = 520,
    }
}
