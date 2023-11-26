

using ERPKeeper.Node.Models.Accounting.ChartOfAccount;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;
using DevExpress.CodeParser;

namespace ERPKeeper.Node.DAL.Accounting
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
        public List<AccountReportGroup> GetAccountReportGroups(AccountTypes AccountType)
        {
            var subTypes = Enum.GetValues(typeof(AccountReportGroup)).Cast<AccountReportGroup>();
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

            fb.Node = erpNodeDBContext.DBName;
            fb.AssetBalance = this.AssetAccounts.Sum(i => i.CurrentBalance);
            fb.LiabilityBalance = this.LiabilityAccounts.Sum(i => i.CurrentBalance);
            fb.EquityBalance = fb.AssetBalance - fb.LiabilityBalance;

            fb.ExpenseBalance = this.organization.FiscalYears.CurrentPeriod.ExpenseBalance;
            fb.IncomeBalance = this.organization.FiscalYears.CurrentPeriod.IncomeBalance;
            return fb;
        }

        public List<AccountItem> OpeningItemList() => this.erpNodeDBContext.AccountItems
                .Where(a => a.OpeningCreditBalance != 0 || a.OpeningDebitBalance != 0)
                .ToList();


        public List<AccountItem> ListRelatedAccounts(AccountItem accountItem) => this.erpNodeDBContext.AccountItems
             .Where(i => i.Type == accountItem.Type && i.SubEnumType == accountItem.SubEnumType)
             .ToList();

        public decimal ItemsBalance(AccountTypes type) => this.GetAccountByType(type).Sum(a => a.CurrentBalance);



        public void RemoveGroup(AccountItem accountItem)
        {
            this.erpNodeDBContext.AccountItems.Remove(accountItem);

            this.SaveChanges();
        }


        public void ReAssignNumber()
        {
            this.ReAssignNumber(AccountTypes.Asset);
            this.ReAssignNumber(AccountTypes.Liability);
            this.ReAssignNumber(AccountTypes.Capital);
            this.ReAssignNumber(AccountTypes.Income);
            this.ReAssignNumber(AccountTypes.Expense);

            this.SaveChanges();
        }

        private void ReAssignNumber(AccountTypes accountType)
        {
            var accounts = erpNodeDBContext.AccountItems
                .Where(a => a.Type == accountType)
                .OrderBy(a => a.SubEnumType)
                .ThenByDescending(a => a.Name)
                .ToList();

            int index = 0;
            string accountTypeString = "0-";

            if (accountType == AccountTypes.Asset)
                accountTypeString = "1-";
            else if (accountType == AccountTypes.Liability)
                accountTypeString = "2-";
            else if (accountType == AccountTypes.Capital)
                accountTypeString = "3-";
            else if (accountType == AccountTypes.Income)
                accountTypeString = "4-";
            else if (accountType == AccountTypes.Expense)
                accountTypeString = "5-";
            else
                accountTypeString = "0-";





            accounts.ForEach(account =>
                {
                    account.Order = ++index;
                    account.No = accountTypeString + account.Order.ToString().PadLeft(2, '0');
                });
        }
        private void ClearBalanceHistory()
        {
            erpNodeDBContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [ERP_Accounting_Account_History_Balance]");
            erpNodeDBContext.SaveChanges();
        }
        private void ClearBalanceHistory(AccountItem account)
        {
            var sqlCommand = "DELETE FROM [dbo].[ERP_Accounting_Account_History_Balance] WHERE  [AccountUid] = '{0}'";
            sqlCommand = string.Format(sqlCommand, account.Uid);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);
            erpNodeDBContext.SaveChanges();
        }
        public void Remove(AccountItem account)
        {
            erpNodeDBContext.AccountItems.Remove(account);
            erpNodeDBContext.SaveChanges();
        }
        public AccountItem Find(Guid AccountUid) => erpNodeDBContext.AccountItems.Find(AccountUid);
        public AccountItem Find(Guid? AccountUid) => erpNodeDBContext.AccountItems.Find(AccountUid);
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

        public void GenerateHistoryBalance(AccountItem accountItem)
        {
            this.ClearBalanceHistory(accountItem);

            var results = erpNodeDBContext.Ledgers
                .Where(GL => GL.AccountUid == accountItem.Uid)
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

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            List<HistoryBalanceItem> HistoryBalanceItems = new List<HistoryBalanceItem>();

            foreach (var result in results)
            {
                var historyBalanceItem = new HistoryBalanceItem()
                {
                    Uid = Guid.NewGuid(),
                    TrnDate = result.TrnDate,
                    AccountUid = accountItem.Uid,
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
            accountItem.CurrentBalanceRecordDate = DateTime.Now;
            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }


        public AccountItem newFolder(AccountTypes type, AccountSubTypes subType, string codeName, string name, string nameEnglish)
        {
            AccountItem item = erpNodeDBContext.AccountItems.Create();

            item.Uid = Guid.NewGuid();
            item.Type = type;
            item.SubEnumType = subType;
            item.CodeName = codeName;
            item.Name = name;

            erpNodeDBContext.AccountItems.Add(item);
            return item;
        }
        public AccountItem newGroup(AccountTypes accountType)
        {
            var newGroup = new AccountItem()
            {
                Uid = Guid.NewGuid(),
                Type = accountType,
            };


            erpNodeDBContext.AccountItems.Add(newGroup);
            erpNodeDBContext.SaveChanges();

            return newGroup;
        }
        public AccountItem newItem(AccountTypes accountType, AccountSubTypes accountSubType)
        {
            var newItem = new AccountItem()
            {
                Type = accountType,
                SubEnumType = accountSubType,
                No = "N/A",
            };

            return newItem;
        }
        public AccountItem newItem(AccountTypes type, AccountSubTypes subType, string codeName, string name, string englishName)
        {
            AccountItem item = erpNodeDBContext.AccountItems.Create();

            item.Uid = Guid.NewGuid();
            item.Type = type;
            item.SubEnumType = subType;
            item.CodeName = codeName;
            item.Name = name;
            item.EnglishName = englishName;

            erpNodeDBContext.AccountItems.Add(item);
            return item;
        }

        public AccountItem Update(AccountItem accountItem)
        {
            var exist = erpNodeDBContext.AccountItems.Find(accountItem.Uid);

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
                exist.SubEnumType = accountItem.SubEnumType;
                exist.Description = accountItem.Description;
                exist.IsFocus = accountItem.IsFocus;
                exist.IsCashEquivalent = accountItem.IsCashEquivalent;
                exist.OpeningDebitBalance = accountItem.OpeningDebitBalance;
                exist.OpeningCreditBalance = accountItem.OpeningCreditBalance;

            }


            if (exist.SubEnumType == AccountSubTypes.Cash || exist.SubEnumType == AccountSubTypes.Bank)
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

            var firstDate = organization.FirstDate;

            var accountBalances = erpNodeDBContext.Ledgers
                .Where(ledger => DbFunctions.TruncateTime(ledger.TrnDate) <= DbFunctions.TruncateTime(DateTime.Now))
                .GroupBy(o => o.AccountUid)
                .Select(go => new
                {
                    AccountUid = go.Key,
                    Account = go.Select(i => i.accountItem).FirstOrDefault(),
                    Credit = go.Sum(i => i.Credit) ?? 0,
                    Debit = go.Sum(i => i.Debit) ?? 0,
                })
                .ToList();


            accountBalances.ForEach(group =>
            {
                group.Account.Debit = group.Debit;
                group.Account.Credit = group.Credit;
                group.Account.CurrentBalanceRecordDate = DateTime.Now;

                //  Console.WriteLine(group.Account.Code + group.Account.Name + ", Dr." + group.Account.Debit + ", Cr." + group.Account.Credit);
            });

            this.SaveChanges();
        }

        public void UpdateBalance(AccountItem accountItem)
        {
            Console.WriteLine("> Update " + accountItem.Name + " Balance.");

            accountItem.ClearBalance();
            erpNodeDBContext.SaveChanges();

            var accountBalances = erpNodeDBContext.Ledgers
                .Where(ledger => ledger.TrnDate <= DateTime.Now)

                .Where(account => account.AccountUid == accountItem.Uid)
                .GroupBy(o => o.AccountUid)
                .Select(go => new
                {
                    AccountUid = go.Key,
                    Account = go.Select(i => i.accountItem).FirstOrDefault(),
                    Credit = go.Sum(i => i.Credit) ?? 0,
                    Debit = go.Sum(i => i.Debit) ?? 0,
                })
                .ToList();


            accountBalances.ForEach(group =>
            {
                group.Account.Credit = group.Credit;
                group.Account.Debit = group.Debit;
                group.Account.CurrentBalanceRecordDate = DateTime.Now;
            });

            this.SaveChanges();
        }

        public void SetAccountLiquidity(Node.DAL.Organization organization)
        {
            this.GetCashEquivalentAccounts().ForEach(a => a.IsLiquidity = true);
            this.InventoryAssetAccounts.ForEach(a => a.IsLiquidity = true);
            this.TaxRelatedAccountItems.ForEach(a => a.IsLiquidity = true);
            this.AccountReceivableAccounts.ForEach(a => a.IsLiquidity = true);
            this.AccountPayableAccounts.ForEach(a => a.IsLiquidity = true);
        }


        public List<HistoryBalanceItem> GetHistoryBalance(AccountItem accountItem, int days = 365)
        {
            var startDate = DateTime.Now.AddDays(-1 * days);

            return erpNodeDBContext.HistoryBalanceItems
                .Where(h => h.AccountUid == accountItem.Uid)
                .ToList();
        }
        public List<AccountItem> GetByType(AccountTypes accountType)
        {
            return erpNodeDBContext.AccountItems
            .Where(account => account.Type == accountType)
            .OrderBy(i => i.No)
            .ToList();
        }
        public List<AccountItem> GetItemBySubType(AccountSubTypes subType) => erpNodeDBContext.AccountItems
              .Where(account => account.SubEnumType == subType)
              .ToList();
        public List<AccountItem> GetAccountByType(AccountTypes accountType) => erpNodeDBContext.AccountItems
                .Where(account => account.Type == accountType)
                .OrderBy(i => i.No)
                .ToList();
        public IEnumerable<AccountItem> GetFocusAccount(AccountTypes? accountType) => erpNodeDBContext.AccountItems
                .Where(account => accountType == null || account.Type == accountType)
                .Where(account => account.IsFocus)
                .OrderBy(i => i.No)
                .ToList();

        public List<AccountItem> ListPreviewAccounts(Guid profileUid) => erpNodeDBContext
                    .PreviewAccounts
                    .Where(pv => (pv.Account.SubEnumType != AccountSubTypes.Cash) && (pv.Account.SubEnumType != AccountSubTypes.Bank))
                    .Where(pv => pv.OwnerProfileGuid == profileUid)
                    .Select(pv => pv.Account)
                    .ToList();
        public List<AccountItem> GetAccountsList() => erpNodeDBContext.AccountItems.ToList();

        public List<AccountItem> AssetAccounts => this.GetAccountByType(AccountTypes.Asset);
        public List<AccountItem> Assets => erpNodeDBContext.AccountItems.Where(a => a.Type == AccountTypes.Asset).ToList();
        public List<AccountItem> AssetAndLiability
        {
            get
            {
                return erpNodeDBContext.AccountItems
              .Where(account => account.Type == AccountTypes.Asset || account.Type == AccountTypes.Liability)
              .ToList();
            }
        }
        public List<AccountItem> GetCashEquivalentAccounts() => erpNodeDBContext.AccountItems
              .Where(account => account.Type == AccountTypes.Asset)
              .Where(account => account.SubEnumType == AccountSubTypes.Bank || account.SubEnumType == AccountSubTypes.Cash || account.IsCashEquivalent)
              .OrderBy(account => account.SubEnumType)
              .ToList();
        public List<AccountItem> GetCashOrBankAccounts() => erpNodeDBContext.AccountItems
            .Where(account => account.Type == AccountTypes.Asset)
            .Where(account => account.SubEnumType == AccountSubTypes.Bank || account.SubEnumType == AccountSubTypes.Cash || account.IsCashEquivalent)
            .OrderBy(account => account.SubEnumType)
            .ToList();
        public List<AccountItem> GetCOGSExpenseAccounts() => this.GetItemBySubType(AccountSubTypes.CostOfGoodsSold);
        public List<AccountItem> EquityAccounts => this.GetAccountByType(Models.Accounting.AccountTypes.Capital);
        public List<AccountItem> ExpenseAccounts => this.GetAccountByType(AccountTypes.Expense);
        public List<AccountItem> TaxRelatedAccountItems => erpNodeDBContext.AccountItems
                 .Where(account => account.SubEnumType == AccountSubTypes.TaxInput || account.SubEnumType == AccountSubTypes.TaxOutput || account.SubEnumType == AccountSubTypes.TaxExpense)

                 .OrderBy(i => i.No)
                 .ToList();
        public List<AccountItem> TaxClosingAccount => erpNodeDBContext.AccountItems
                 .Where(account => account.SubEnumType == AccountSubTypes.TaxPayable || account.SubEnumType == AccountSubTypes.TaxReceivable)

                 .OrderBy(i => i.No)
                 .ToList();
        public List<AccountItem> ListTaxAccounts(Models.Taxes.Enums.TaxDirection direction)
        {

            if (direction == Models.Taxes.Enums.TaxDirection.Input)
                return erpNodeDBContext.AccountItems.Where(account => account.SubEnumType == AccountSubTypes.TaxInput)

                             .OrderBy(i => i.No)
                             .ToList();
            else
                return erpNodeDBContext.AccountItems.Where(account => account.SubEnumType == AccountSubTypes.TaxOutput)

                                .OrderBy(i => i.No)
                                .ToList();


        }
        public List<AccountItem> FixedAssets => this.GetItemBySubType(AccountSubTypes.FixedAsset);
        public List<AccountItem> IncomeAccounts => this.GetAccountByType(AccountTypes.Income);
        public List<AccountItem> InventoryAssetAccounts => this.GetItemBySubType(AccountSubTypes.Inventory);
        public List<AccountItem> LiabilityAccounts => this.GetAccountByType(AccountTypes.Liability);
        public List<AccountItem> CurrentLiabilityAccounts => this.GetItemBySubType(AccountSubTypes.CurrentLiability);
        public List<AccountItem> AccountReceivableAccounts => this.GetItemBySubType(AccountSubTypes.AccountReceivable);
        public List<AccountItem> AccountPayableAccounts => this.GetItemBySubType(AccountSubTypes.AccountPayable);
        public List<AccountItem> TaxInputAccounts => this.GetItemBySubType(AccountSubTypes.TaxInput);
        public List<AccountItem> TaxOutputAccounts => this.GetItemBySubType(AccountSubTypes.TaxOutput);
        public List<AccountItem> TaxPayableAccounts => this.GetItemBySubType(AccountSubTypes.TaxPayable);
        public List<AccountItem> TaxReceivableAccounts => this.GetItemBySubType(AccountSubTypes.TaxReceivable);
        public List<AccountItem> TaxInputAndTaxExpenseAccounts => erpNodeDBContext.AccountItems
                 .Where(account => account.SubEnumType == AccountSubTypes.TaxInput || account.SubEnumType == AccountSubTypes.TaxExpense)

                 .OrderBy(i => i.No)
                 .ToList();

    }
}

