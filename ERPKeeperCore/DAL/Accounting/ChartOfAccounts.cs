

using KeeperCore.ERPNode.Models;
using KeeperCore.ERPNode.Models.Enums;
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
        

        public Account newFolder(AccountTypes type, AccountSubTypes subType, string codeName, string name, string nameEnglish)
        {
            Account item = new()
            {
                Type = type,
                SubType = subType,
                IsFolder = true,
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
       
      

    
        public List<Account> GetByType(AccountTypes accountType)
        {
            return erpNodeDBContext.AccountItems
            .Where(account => account.Type == accountType)
            .OrderByDescending(i => i.IsFolder)
            .ThenBy(i => i.No)
            .ToList();
        }
     
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
        public List<Account> ListTaxAccounts(Models.Enums.TaxDirection direction)
        {

            if (direction == Models.Enums.TaxDirection.Input)
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

