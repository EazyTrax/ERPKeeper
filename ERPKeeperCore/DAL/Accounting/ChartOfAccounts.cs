using ERPKeeperCore.Enterprise.DBContext;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;


namespace ERPKeeperCore.Enterprise.DAL.Accounting
{
    public class ChartOfAccounts : ERPNodeDalRepository
    {
        public ChartOfAccounts(EnterpriseRepo organization) : base(organization)
        {

        }




        public List<Account> ListRelatedAccounts(Account accountItem) =>
            this.erpNodeDBContext.Accounts
             .Where(i => i.Type == accountItem.Type && i.SubType == accountItem.SubType)
             .ToList();

        public Decimal ItemsBalance(AccountTypes type) => this.GetAccountByType(type).Sum(a => a.CurrentBalance);



        public void RemoveGroup(Account accountItem)
        {
            this.erpNodeDBContext.Accounts.Remove(accountItem);

            this.SaveChanges();
        }


        public void ReAssignNumber()
        {
            this.ReAssignNumber(null, AccountTypes.Asset);
            this.ReAssignNumber(null, AccountTypes.Liability);
            this.ReAssignNumber(null, AccountTypes.Capital);
            this.ReAssignNumber(null, AccountTypes.Income);
            this.ReAssignNumber(null, AccountTypes.Expense);

            this.SaveChanges();
        }

        private void ReAssignNumber(Guid? parentId, AccountTypes accountType)
        {

        }
        private void ClearBalanceHistory()
        {
            erpNodeDBContext.Database.ExecuteSqlRaw("TRUNCATE TABLE [ERP_Accounting_Account_History_Balance]");
            erpNodeDBContext.SaveChanges();
        }
        private void ClearBalanceHistory(Account account)
        {
            var sqlCommand = "DELETE FROM [dbo].[ERP_Accounting_Account_History_Balance] WHERE  [AccountId] = '{0}'";
            sqlCommand = string.Format(sqlCommand, account.Id);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
            erpNodeDBContext.SaveChanges();
        }
        public void Remove(Account account)
        {
            erpNodeDBContext.Accounts.Remove(account);
            erpNodeDBContext.SaveChanges();
        }
        public Account? Find(Guid AccountId)
        {
            return erpNodeDBContext.Accounts.Find(AccountId);
        }
        public Account Update(Account accountItem)
        {
            var exist = erpNodeDBContext.Accounts.Find(accountItem.Id);

            if (exist == null)
            {
                erpNodeDBContext.Accounts.Add(accountItem);
                exist = accountItem;
            }
            else
            {
                exist.No = accountItem.No;
                exist.Name = accountItem.Name;
                exist.IsLiquidity = accountItem.IsLiquidity;
                exist.SubType = accountItem.SubType;
                exist.Description = accountItem.Description;
                exist.IsCashEquivalent = accountItem.IsCashEquivalent;
            }


            if (exist.SubType == AccountSubTypes.Asset_Cash || exist.SubType == AccountSubTypes.Asset_Bank)
                exist.IsCashEquivalent = true;


            erpNodeDBContext.SaveChanges();

            return exist;
        }
        public List<Account> GetByType(AccountTypes accountType)
        {
            return erpNodeDBContext.Accounts
            .Where(account => account.Type == accountType)
            .OrderByDescending(i => i.IsFolder)
            .ThenBy(i => i.No)
            .ToList();
        }
        public List<Account> GetItemBySubType(AccountSubTypes subType) => erpNodeDBContext.Accounts
                .Where(account => account.SubType == subType)
        .ToList();

        public List<Account> GetItemBySubType(AccountSubTypes subType, AccountSubTypes subtype_other) => erpNodeDBContext.Accounts
              .Where(account => account.SubType == subType || account.SubType == subtype_other)
              .ToList();


        public List<Account> GetAccountByType(AccountTypes accountType) => erpNodeDBContext.Accounts
                .Where(account => account.Type == accountType)
                .OrderBy(i => i.No)
                .ToList();
        public IEnumerable<Account> GetFocusAccount(AccountTypes? accountType) => erpNodeDBContext.Accounts
                .Where(account => accountType == null || account.Type == accountType)
                .OrderBy(i => i.No)
                .ToList();
        public List<Account> All() => erpNodeDBContext.Accounts
            .Where(i => i.IsFolder == false)
            .ToList();
        public List<Account> Folders => erpNodeDBContext.Accounts
            .Where(i => i.IsFolder)
            .ToList();

        public List<Account> AssetsAndExpenses => erpNodeDBContext.Accounts
             .Where(a => a.Type == AccountTypes.Asset || a.Type == AccountTypes.Expense)
             .ToList();


        public List<Account> Assets => erpNodeDBContext.Accounts
            .Where(a => a.Type == AccountTypes.Asset)
            .ToList();

        public List<Account> AssetAndLiability
        {
            get
            {
                return erpNodeDBContext.Accounts
              .Where(account => account.Type == AccountTypes.Asset || account.Type == AccountTypes.Liability)

              .ToList();
            }
        }
        public List<Account> GetCashEquivalentAccounts() => erpNodeDBContext.Accounts
              .Where(account => account.Type == AccountTypes.Asset)

              .Where(account => account.SubType == AccountSubTypes.Asset_Bank || account.SubType == AccountSubTypes.Asset_Cash || account.IsCashEquivalent)
              .OrderBy(account => account.SubType)
              .ToList();
        public List<Account> GetCashOrBankAccounts() => erpNodeDBContext.Accounts
            .Where(account => account.Type == AccountTypes.Asset)

            .Where(account => account.SubType == AccountSubTypes.Asset_Bank || account.SubType == AccountSubTypes.Asset_Cash || account.IsCashEquivalent)
            .OrderBy(account => account.SubType)
            .ToList();
        public List<Account> GetCOGSExpenseAccounts() => this.GetItemBySubType(AccountSubTypes.Expense_CostOfGoodsSold);
        public List<Account> EquityAccounts => this.GetAccountByType(AccountTypes.Capital);
        public List<Account> ExpenseAccounts => this.GetAccountByType(AccountTypes.Expense);
        public List<Account> PurchaseExpenseOrInventoryAssetAccounts =>
               this.GetAccountByType(AccountTypes.Expense)
               .Concat(this.GetItemBySubType(AccountSubTypes.Asset_Inventory))
               .ToList();

        public List<Account> TaxRelatedAccountItems => erpNodeDBContext.Accounts
                 .Where(account => account.SubType == AccountSubTypes.Asset_TaxInput
                 || account.SubType == AccountSubTypes.Liability_TaxOutput
                 || account.SubType == AccountSubTypes.Expense_NonRefundableTax)
                 .OrderBy(i => i.No)
                 .ToList();
        public List<Account> TaxClosingAccount => erpNodeDBContext.Accounts
                 .Where(account =>
                 account.SubType == AccountSubTypes.Liability_TaxPayable ||
                 account.SubType == AccountSubTypes.Asset_TaxReceivable)

                 .OrderBy(i => i.No)
                 .ToList();
        public List<Account> ListTaxAccounts(TaxDirection direction)
        {

            if (direction == TaxDirection.Input)
                return erpNodeDBContext.Accounts.Where(account => account.SubType == AccountSubTypes.Asset_TaxInput)

                             .OrderBy(i => i.No)
                             .ToList();
            else
                return erpNodeDBContext.Accounts.Where(account => account.SubType == AccountSubTypes.Liability_TaxOutput)

                                .OrderBy(i => i.No)
                                .ToList();
        }

        public void Refresh_CurrentBalance()
        {
            var fistDate = organization.FiscalYears.FirstPeriod.StartDate;

            var accountBalances = erpNodeDBContext.TransactionLedgers
                .Where(l => l.Transaction.Date >= fistDate)
                .GroupBy(t => t.AccountId)
                .Select(t => new { AccountId = t.Key, Debit = t.Sum(i => i.Debit), Credit = t.Sum(i => i.Credit) })
                .ToList();

            foreach (var accBalance in accountBalances)
            {
                var account = erpNodeDBContext.Accounts.Find(accBalance.AccountId);
                if (account != null)
                {
                    account.CurrentCredit = accBalance.Credit;
                    account.CurrentDebit = accBalance.Debit;
                    account.BalanceCalulatedDate = DateTime.Today;

                    erpNodeDBContext.SaveChanges();
                }

            }
        }

        public void Update_CurrentBalance(List<Account> accounts)
        {
            var firstDate = organization.FiscalYears.FirstPeriod.StartDate;
            var accountIds = accounts.Select(a => a.Id).ToList();

            var accountBalances = erpNodeDBContext.TransactionLedgers
                .Where(l => accountIds.Contains(l.AccountId) && l.Transaction.Date >= firstDate)
                .GroupBy(t => t.AccountId)
                .Select(t => new { AccountId = t.Key, Debit = t.Sum(i => i.Debit), Credit = t.Sum(i => i.Credit) })
                .ToList();

            foreach (var accBalance in accountBalances)
            {
                var account = accounts.FirstOrDefault(a => a.Id == accBalance.AccountId);
                if (account != null)
                {
                    account.CurrentCredit = accBalance.Credit;
                    account.CurrentDebit = accBalance.Debit;
                    account.BalanceCalulatedDate = DateTime.Today;
                }
            }

            // Save changes once after all updates are made
            erpNodeDBContext.SaveChanges();
        }





        public void GenerateHistoryBalance()
        {
            erpNodeDBContext.Accounts.ToList()
                  .ForEach(model =>
                  {
                      model.CreateHostoriesBalances();
                      erpNodeDBContext.SaveChanges();
                  });
        }

        public void CreateOpeningJournalEntry()
        {
            var openingJournalEntryType = erpNodeDBContext
                .JournalEntryTypes
                .FirstOrDefault(j => j.Name == "Opening");

            if (openingJournalEntryType == null)
            {
                openingJournalEntryType = new JournalEntryType
                {
                    Name = "Opening",
                    Description = "Opening Journal Entry",
                    IsSystem = true
                };
                erpNodeDBContext.JournalEntryTypes.Add(openingJournalEntryType);
                erpNodeDBContext.SaveChanges();
            }

            var openingJournalEntry = erpNodeDBContext
                .JournalEntries
                .FirstOrDefault(j => j.JournalEntryTypeId == openingJournalEntryType.Id);

            if (openingJournalEntry == null)
            {
                openingJournalEntry = new JournalEntry
                {
                    Date = erpNodeDBContext.FiscalYears.OrderBy(f => f.StartDate).FirstOrDefault().StartDate,
                    Description = "Opening Journal Entry",
                    JournalEntryTypeId = openingJournalEntryType.Id,
                };
                erpNodeDBContext.JournalEntries.Add(openingJournalEntry);
                erpNodeDBContext.SaveChanges();
            }

            openingJournalEntry.RemoveAllItems();
            erpNodeDBContext.SaveChanges();

            var accountsWithOpening = erpNodeDBContext.Accounts
                .Where(a => a.OpeningCredit != 0 || a.OpeningDebit != 0)
                .ToList();

            foreach (var acc in accountsWithOpening)
            {
                openingJournalEntry.AddAcount(acc.Id, acc.OpeningDebit, acc.OpeningCredit);
            }
            erpNodeDBContext.SaveChanges();
        }

        public void Refresh_HostoriesBalances()
        {
            erpNodeDBContext.Accounts
                     .ToList()
                     .ForEach(model =>
                     {
                         model.CreateHostoriesBalances();
                         erpNodeDBContext.SaveChanges();
                     });
        }

        public List<Account> IncomeExpenseAccounts
        {
            get
            {
                return erpNodeDBContext.Accounts
              .Where(account => account.Type == AccountTypes.Asset || account.Type == AccountTypes.Expense).ToList();
            }
        }
        public List<Account> IncomeAccounts => this.GetAccountByType(AccountTypes.Income);
        public List<Account> InventoryAssetAccounts => this.GetItemBySubType(AccountSubTypes.Asset_Inventory);
        public List<Account> LiabilityAccounts => this.GetAccountByType(AccountTypes.Liability);
        public List<Account> CurrentLiabilityAccounts => this.GetItemBySubType(AccountSubTypes.Liability_Current);
        public List<Account> AccountReceivableAccounts => this.GetItemBySubType(AccountSubTypes.Asset_AccountReceivable);
        public List<Account> AccountPayableAccounts => this.GetItemBySubType(AccountSubTypes.Liability_AccountPayable);
        public List<Account> TaxInputAccounts => this.GetItemBySubType(AccountSubTypes.Asset_TaxInput);
        public List<Account> TaxOutputAccounts => this.GetItemBySubType(AccountSubTypes.Liability_TaxOutput);
        public List<Account> TaxPayableAccounts => this.GetItemBySubType(AccountSubTypes.Liability_TaxPayable);
        public List<Account> TaxReceivableAccounts => this.GetItemBySubType(AccountSubTypes.Asset_TaxReceivable);
        public List<Account> TaxInputAndTaxExpenseAccounts => erpNodeDBContext.Accounts
                 .Where(account =>
                 account.SubType == AccountSubTypes.Asset_TaxInput ||
                 account.SubType == AccountSubTypes.Expense_NonRefundableTax)

                 .OrderBy(i => i.No)
                 .ToList();
    }
}