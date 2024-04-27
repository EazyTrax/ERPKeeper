namespace ERPKeeperCore.Enterprise.Models.Security.Enums
{
    public enum AccessLevel
    {
        None = 0,
        Viewer = 1,
        TransactionMaker = 2,
        Accountant = 3,
        Auditor = 9,
        Admin = 15,
    }
}
