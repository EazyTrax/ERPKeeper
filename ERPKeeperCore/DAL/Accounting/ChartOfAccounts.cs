

using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.ChartOfAccount;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace KeeperCore.ERPNode.DAL.Accounting
{
    public class ChartOfAccounts : ERPNodeDalRepository
    {
        public ChartOfAccounts(Organization organization) : base(organization)
        {

        }


        public List<AccountSubTypes> GetSubType(AccountTypes AccountType)
        {
            var subTypes = Enum.GetValues(typeof(AccountSubTypes)).Cast<AccountSubTypes>();
            switch (AccountType)
            {
                case AccountTypes.Asset:
                    return subTypes.Where(subType => (int)subType >= 100 & (int)subType <= 199).ToList();

                case AccountTypes.Liability:
                    return subTypes.Where(subType => (int)subType >= 200 & (int)subType <= 299).ToList();

                case AccountTypes.Capital:
                    return subTypes.Where(subType => (int)subType >= 300 & (int)subType <= 399).ToList();

                case AccountTypes.Income:
                    return subTypes.Where(subType => (int)subType >= 400 & (int)subType <= 499).ToList();

                case AccountTypes.Expense:
                    return subTypes.Where(subType => (int)subType >= 500 & (int)subType <= 599).ToList();
            }

            return subTypes.ToList();
        }

        public FinancialBalance GetFinancial()
        {
            FinancialBalance fb = new FinancialBalance();


            fb.AssetBalance = this.AssetAccounts.Sum(i => i.CurrentBalance);
            fb.LiabilityBalance = this.LiabilityAccounts.Sum(i => i.CurrentBalance);
            fb.EquityBalance = fb.AssetBalance - fb.LiabilityBalance;

            fb.ExpenseBalance = this.organization.FiscalYears.CurrentPeriod.Expense;
            fb.IncomeBalance = this.organization.FiscalYears.CurrentPeriod.Income;
            return fb;
        }

        public List<Account> OpeningItemList() => this.erpNodeDBContext.AccountItems
                .Where(a => a.OpeningCreditBalance != 0 || a.OpeningDebitBalance != 0)
                .ToList();


        public List<Account> ListRelatedAccounts(Account accountItem) => this.erpNodeDBContext.AccountItems
             .Where(i => i.IsFolder == false)
             .Where(i => i.Type == accountItem.Type && i.SubType == accountItem.SubType)
             .ToList();

        public decimal ItemsBalance(AccountTypes type) => this.GetAccountByType(type).Sum(a => a.CurrentBalance);



        public void RemoveGroup(Account accountItem)
        {
            this.erpNodeDBContext.AccountItems.Remove(accountItem);

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
            var accounts = erpNodeDBContext.AccountItems
                .Where(a => a.ParentId == parentId)
                .Where(a => a.Type == accountType)
                .OrderByDescending(a => a.IsFolder)
                .ThenByDescending(a => a.SubType)
                .ThenByDescending(a => a.Name)
                .ToList();

            int index = 0;

            accounts.ForEach(account =>
            {
                account.Order = ++index;

                if (account.Parent == null)
                    account.No = accountType.ToString().Substring(0, 1) + account.Order.ToString().PadLeft(2, '0');
                else
                    account.No = account.Parent?.No + "-" + account.Order.ToString().PadLeft(2, '0');

                if (account.IsFolder)
                    this.ReAssignNumber(account.Id, account.Type);

            });
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
            erpNodeDBContext.AccountItems.Remove(account);
            erpNodeDBContext.SaveChanges();
        }
        public Account Find(Guid AccountId) => erpNodeDBContext.AccountItems.Find(AccountId);
        public Account Find(Guid? AccountId) => erpNodeDBContext.AccountItems.Find(AccountId);
        public void GenerateHistoryBalance()
        {
            Console.WriteLine("> {0} Generate History Balance", DateTime.Now.ToLongTimeString());
            this.ClearBalanceHistory();



            var accounts = erpNodeDBContext.AccountItems.ToList();
            using (var progress = new Helpers.ProgressBar())
            {
                var currentIndex = 0;
                accounts.ForEach(account =>
                {
                    progress.Report(currentIndex++, accounts.Count);
                    this.GenerateHistoryBalance(account);
                });
            }


        }

        public void GenerateHistoryBalance(Account accountItem)
        {
            this.ClearBalanceHistory(accountItem);

            var results = erpNodeDBContext.Ledgers
                .Where(GL => GL.AccountId == accountItem.Id)
                .Where(GL => GL.TrnDate <= DateTime.Now)
                .GroupBy(o => new { o.TrnDate })
                .Select(go => new
                {
                    TrnDate = go.Key.TrnDate,
                    Credit = go.Sum(ii => ii.Credit) ?? 0,
                    Debit = go.Sum(ii => ii.Debit) ?? 0,
                    Count = go.Count()
                })
                .OrderBy(r => r.TrnDate)
                .ToList();

            decimal balance = 0;

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            List<AccountBalance> HistoryBalanceItems = new List<AccountBalance>();

            foreach (var result in results)
            {
                var historyBalanceItem = new AccountBalance()
                {
                    Id = Guid.NewGuid(),
                    TrnDate = result.TrnDate,
                    AccountId = accountItem.Id,
                    AccountItem = accountItem,
                    Debit = result.Debit,
                    Credit = result.Credit,
                    Count = result.Count
                };

                balance = balance + historyBalanceItem.Total;
                historyBalanceItem.Balance = balance;
                HistoryBalanceItems.Add(historyBalanceItem);
            }

            erpNodeDBContext.HistoryBalanceItems.AddRange(HistoryBalanceItems);
            accountItem.RecordedDate = DateTime.Now;
            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }


        public Account newFolder(AccountTypes type, AccountSubTypes subType, string codeName, string name, string nameEnglish)
        {
            Account item = new()
            {
                Type = type,
                SubType = subType,
                IsFolder = true,
                CodeName = codeName,
                Name = name
            };


            erpNodeDBContext.AccountItems.Add(item);
            return item;
        }
        public Account newGroup(AccountTypes accountType)
        {
            var newGroup = new Account()
            {
                Id = Guid.NewGuid(),
                Type = accountType,
                IsFolder = true
            };


            erpNodeDBContext.AccountItems.Add(newGroup);
            erpNodeDBContext.SaveChanges();

            return newGroup;
        }
        public Account newItem(AccountTypes accountType, AccountSubTypes accountSubType)
        {
            var newItem = new Account()
            {
                Type = accountType,
                SubType = accountSubType,
                OpeningBalance = 0,
                No = "N/A",
            };

            return newItem;
        }
        public Account newItem(AccountTypes type, AccountSubTypes subType, string codeName, string name, string englishName)
        {
            Account item = new()
            {
                Type = type,
                SubType = subType,
                IsFolder = false,
                CodeName = codeName,
                Name = name,
                EnglishName = englishName
            };




            erpNodeDBContext.AccountItems.Add(item);
            return item;
        }

        public Account Update(Account accountItem)
        {
            var exist = erpNodeDBContext.AccountItems.Find(accountItem.Id);

            if (exist == null)
            {
                erpNodeDBContext.AccountItems.Add(accountItem);
                exist = accountItem;
            }
            else
            {
                exist.No = accountItem.No;
                exist.Name = accountItem.Name;
                exist.IsLiquidity = accountItem.IsLiquidity;
                exist.ParentId = accountItem.ParentId;
                exist.SubType = accountItem.SubType;
                exist.Description = accountItem.Description;
                exist.IsFocus = accountItem.IsFocus;
                exist.IsCashEquivalent = accountItem.IsCashEquivalent;
                exist.OpeningDebitBalance = accountItem.OpeningDebitBalance;
                exist.OpeningCreditBalance = accountItem.OpeningCreditBalance;

            }


            if (exist.SubType == AccountSubTypes.Cash || exist.SubType == AccountSubTypes.Bank)
                exist.IsCashEquivalent = true;



            erpNodeDBContext.SaveChanges();

            return exist;
        }
        private void ClearBalance()
        {
            this.GetAccountsList().ToList().ForEach(Account => Account.ClearBalance());
        }

        public void UpdateBalance()
        {
            Console.WriteLine("> Update Account Balance @ {0}", DateTime.Now.ToLongTimeString());
            this.ClearBalance();
            this.erpNodeDBContext.SaveChanges();

            var firstDate = organization.DataItems.FirstDate;

            var accountBalances = erpNodeDBContext.Ledgers
                .Where(ledger => ledger.TrnDate <= DateTime.Now)
                .GroupBy(o => o.AccountId)
                .Select(go => new
                {
                    AccountId = go.Key,
                    Account = go.Select(i => i.accountItem).FirstOrDefault(),
                    Credit = go.Sum(i => i.Credit) ?? 0,
                    Debit = go.Sum(i => i.Debit) ?? 0,
                })
                .ToList();


            accountBalances.ForEach(group =>
            {
                group.Account.Debit = group.Debit;
                group.Account.Credit = group.Credit;
                group.Account.RecordedDate = DateTime.Now;

                //  Console.WriteLine(group.Account.Code + group.Account.Name + ", Dr." + group.Account.Debit + ", Cr." + group.Account.Credit);
            });

            this.SaveChanges();
        }
        public void UpdateBalance(Account accountItem)
        {
            Console.WriteLine("> Update " + accountItem.Name + " Balance.");

            accountItem.ClearBalance();
            erpNodeDBContext.SaveChanges();

            var accountBalances = erpNodeDBContext.Ledgers
                .Where(ledger => ledger.TrnDate <= DateTime.Now)

                .Where(account => account.AccountId == accountItem.Id)
                .GroupBy(o => o.AccountId)
                .Select(go => new
                {
                    AccountId = go.Key,
                    Account = go.Select(i => i.accountItem).FirstOrDefault(),
                    Credit = go.Sum(i => i.Credit) ?? 0,
                    Debit = go.Sum(i => i.Debit) ?? 0,
                })
                .ToList();


            accountBalances.ForEach(group =>
            {
                group.Account.Credit = group.Credit;
                group.Account.Debit = group.Debit;
                group.Account.RecordedDate = DateTime.Now;
            });

            this.SaveChanges();
        }
        public void SetAccountLiquidity(ERPNode.DAL.Organization organization)
        {
            this.GetCashEquivalentAccounts().ForEach(a => a.IsLiquidity = true);
            this.InventoryAssetAccounts.ForEach(a => a.IsLiquidity = true);
            this.TaxRelatedAccountItems.ForEach(a => a.IsLiquidity = true);
            this.AccountReceivableAccounts.ForEach(a => a.IsLiquidity = true);
            this.AccountPayableAccounts.ForEach(a => a.IsLiquidity = true);
        }


        public List<AccountBalance> GetHistoryBalance(Account accountItem, int days = 365)
        {
            var startDate = DateTime.Now.AddDays(-1 * days);

            return erpNodeDBContext.HistoryBalanceItems
                .Where(h => h.AccountId == accountItem.Id)
                .ToList();
        }
        public List<Account> GetByType(AccountTypes accountType)
        {
            return erpNodeDBContext.AccountItems
            .Where(account => account.Type == accountType)
            .OrderByDescending(i => i.IsFolder)
            .ThenBy(i => i.No)
            .ToList();
        }
        public List<Account> GetFolderByTypes(AccountTypes accountType) => erpNodeDBContext.AccountItems.Where(account => account.Type == accountType && (account.IsFolder == true || account.SubType == AccountSubTypes.FixedAsset)).ToList();
        public List<Account> GetItemBySubType(AccountSubTypes subType) => erpNodeDBContext.AccountItems
                .Where(account => account.SubType == subType)
                .Where(account => account.IsFolder == false)
                .ToList();
        public List<Account> GetAccountByType(AccountTypes accountType) => erpNodeDBContext.AccountItems
                .Where(account => account.Type == accountType)
                .Where(account => account.IsFolder == false)
                .OrderBy(i => i.No)
                .ToList();
        public IEnumerable<Account> GetFocusAccount(AccountTypes? accountType) => erpNodeDBContext.AccountItems
                .Where(account => accountType == null || account.Type == accountType)
                .Where(account => account.IsFocus)
                .OrderBy(i => i.No)
                .ToList();


        public List<Account> GetAccountsList() => erpNodeDBContext.AccountItems
            .Where(i => i.IsFolder == false)
            .ToList();
        public List<Account> Folders => erpNodeDBContext.AccountItems
            .Where(i => i.IsFolder)
            .ToList();
        public List<Account> AssetAccounts => this.GetAccountByType(AccountTypes.Asset);
        public List<Account> Assets => erpNodeDBContext.AccountItems.Where(a => a.Type == AccountTypes.Asset).ToList();
        public List<Account> AssetAndLiability
        {
            get
            {
                return erpNodeDBContext.AccountItems
              .Where(account => account.Type == AccountTypes.Asset || account.Type == AccountTypes.Liability)
              .Where(account => account.IsFolder == false)
              .Where(account => account.IsFolder == false).ToList();
            }
        }
        public List<Account> GetCashEquivalentAccounts() => erpNodeDBContext.AccountItems
              .Where(account => account.Type == AccountTypes.Asset)
              .Where(account => account.IsFolder == false)
              .Where(account => account.SubType == AccountSubTypes.Bank || account.SubType == AccountSubTypes.Cash || account.IsCashEquivalent)
              .OrderBy(account => account.SubType)
              .ToList();
        public List<Account> GetCashOrBankAccounts() => erpNodeDBContext.AccountItems
            .Where(account => account.Type == AccountTypes.Asset)
            .Where(account => account.IsFolder == false)
            .Where(account => account.SubType == AccountSubTypes.Bank || account.SubType == AccountSubTypes.Cash || account.IsCashEquivalent)
            .OrderBy(account => account.SubType)
            .ToList();
        public List<Account> GetCOGSExpenseAccounts() => this.GetItemBySubType(AccountSubTypes.CostOfGoodsSold);
        public List<Account> EquityAccounts => this.GetAccountByType(AccountTypes.Capital);
        public List<Account> ExpenseAccounts => this.GetAccountByType(AccountTypes.Expense);
        public List<Account> TaxRelatedAccountItems => erpNodeDBContext.AccountItems
                 .Where(account => account.SubType == AccountSubTypes.TaxInput || account.SubType == AccountSubTypes.TaxOutput || account.SubType == AccountSubTypes.TaxExpense)
                 .Where(account => account.IsFolder == false)
                 .OrderBy(i => i.No)
                 .ToList();
        public List<Account> TaxClosingAccount => erpNodeDBContext.AccountItems
                 .Where(account => account.SubType == AccountSubTypes.TaxPayable || account.SubType == AccountSubTypes.TaxReceivable)
                 .Where(account => account.IsFolder == false)
                 .OrderBy(i => i.No)
                 .ToList();
        public List<Account> ListTaxAccounts(Models.Taxes.Enums.TaxDirection direction)
        {

            if (direction == Models.Taxes.Enums.TaxDirection.Input)
                return erpNodeDBContext.AccountItems.Where(account => account.SubType == AccountSubTypes.TaxInput)
                             .Where(account => account.IsFolder == false)
                             .OrderBy(i => i.No)
                             .ToList();
            else
                return erpNodeDBContext.AccountItems.Where(account => account.SubType == AccountSubTypes.TaxOutput)
                                .Where(account => account.IsFolder == false)
                                .OrderBy(i => i.No)
                                .ToList();


        }
        public List<Account> FixedAssets => this.GetItemBySubType(AccountSubTypes.FixedAsset);
        public List<Account> IncomeAccounts => this.GetAccountByType(AccountTypes.Income);
        public List<Account> InventoryAssetAccounts => this.GetItemBySubType(AccountSubTypes.Inventory);
        public List<Account> LiabilityAccounts => this.GetAccountByType(AccountTypes.Liability);
        public List<Account> CurrentLiabilityAccounts => this.GetItemBySubType(AccountSubTypes.CurrentLiability);
        public List<Account> AccountReceivableAccounts => this.GetItemBySubType(AccountSubTypes.AccountReceivable);
        public List<Account> AccountPayableAccounts => this.GetItemBySubType(AccountSubTypes.AccountPayable);
        public List<Account> TaxInputAccounts => this.GetItemBySubType(AccountSubTypes.TaxInput);
        public List<Account> TaxOutputAccounts => this.GetItemBySubType(AccountSubTypes.TaxOutput);
        public List<Account> TaxPayableAccounts => this.GetItemBySubType(AccountSubTypes.TaxPayable);
        public List<Account> TaxReceivableAccounts => this.GetItemBySubType(AccountSubTypes.TaxReceivable);
        public List<Account> TaxInputAndTaxExpenseAccounts => erpNodeDBContext.AccountItems
                 .Where(account => account.SubType == AccountSubTypes.TaxInput || account.SubType == AccountSubTypes.TaxExpense)
                 .Where(account => account.IsFolder == false)
                 .OrderBy(i => i.No)
                 .ToList();

    }
}

