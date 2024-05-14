using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Enterprise.DAL.Accounting
{
    public class SystemAccounts : ERPNodeDalRepository
    {
        public SystemAccounts(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<DefaultAccount> GetAll()
        {
            return erpNodeDBContext.DefaultAccounts.ToList();
        }

        public Account Cash => this.GetAccount(DefaultAccountType.Cash);
        public Account BankFee => this.GetAccount(DefaultAccountType.Expense_BankFee);
        public Account COSG => this.GetAccount(DefaultAccountType.CostOfGoodSold);
        public Account OverPayment => this.GetAccount(DefaultAccountType.OverRecivePayment);
        public Account RetainedEarning => this.GetAccount(DefaultAccountType.RetainedEarning);
        public Account AccountPayable => this.GetAccount(DefaultAccountType.Liability_AccountPayable);
        public Account AccountReceivable => this.GetAccount(DefaultAccountType.Asset_AccountReceivable);
        public Account OpeningBalanceEquity => this.GetAccount(DefaultAccountType.OpeningBalanceEquity);

        public void Add(DefaultAccount defaultAccountItem)
        {
            erpNodeDBContext.DefaultAccounts.Add(defaultAccountItem);
            erpNodeDBContext.SaveChanges();
        }

        public Account EquityStock => this.GetAccount(DefaultAccountType.EquityStock);
        public Account DiscountGiven => this.GetAccount(DefaultAccountType.DiscountGiven);
        public Account DiscountTaken => this.GetAccount(DefaultAccountType.Income_DiscountTaken);
        public Account Income => this.GetAccount(DefaultAccountType.Income);
        public Account Expense => this.GetAccount(DefaultAccountType.Expense);

        public Account GetAccount(DefaultAccountType type)
        {
            var defaultAccountItem = erpNodeDBContext.DefaultAccounts.Find(type);
            if (defaultAccountItem != null)
                return defaultAccountItem.Account;
            else
                return null;
        }

        public DefaultAccount Find(DefaultAccountType type) => erpNodeDBContext.DefaultAccounts.Find(type);



        public void SetIfUnAssign(DefaultAccountType defaultAccountType, Account accountItem)
        {
            if (this.GetAccount(defaultAccountType) == null)
            {
                this.Set(defaultAccountType, accountItem);
            }
        }

        public void Set(DefaultAccountType defaultAccountType, Account accountItem)
        {
            if (accountItem != null)
            {
                var defaultAccountItem = erpNodeDBContext.DefaultAccounts.Find(defaultAccountType);

                if (defaultAccountItem != null)
                {
                    defaultAccountItem.AccountId = accountItem.Id;
                    defaultAccountItem.Account = accountItem;
                    defaultAccountItem.LastUpdate = DateTime.Now;
                }
                else
                {
                    defaultAccountItem = new DefaultAccount()
                    {
                        Type = defaultAccountType,
                        Account = accountItem,
                        AccountId = accountItem.Id,
                        LastUpdate = DateTime.Now
                    };
                    erpNodeDBContext.DefaultAccounts.Add(defaultAccountItem);
                }
            }
            erpNodeDBContext.SaveChanges();

        }

        public void Create(DefaultAccountType accountType)
        {
            var defaultAccountItem = erpNodeDBContext.DefaultAccounts.Find(accountType);

            if (defaultAccountItem == null)
            {
                defaultAccountItem = new DefaultAccount()
                {
                    Type = accountType,
                    LastUpdate = DateTime.Now
                };

                erpNodeDBContext.DefaultAccounts.Add(defaultAccountItem);
                erpNodeDBContext.SaveChanges();
            }
        }



        public void AutoAssignSystemAccount()
        {
            this.AutoCreateSystemAccount();

            Console.WriteLine("=> Auto Assign System Account");
            var accounts = erpNodeDBContext.Accounts.Where(i => i.IsFolder == false).ToList();

            this.SetIfUnAssign(DefaultAccountType.Income, accounts.Where(i => i.Type == AccountTypes.Income).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Cash, accounts.Where(i => i.SubType == AccountSubTypes.Asset_Cash).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Liability_AccountPayable, accounts.Where(i => i.SubType == AccountSubTypes.Liability_AccountPayable).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Expense, accounts.Where(i => i.SubType == AccountSubTypes.Expense).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.RetainedEarning, accounts.Where(i => i.SubType == AccountSubTypes.Equity_RetainEarning).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EquityStock, accounts.Where(i => i.SubType == AccountSubTypes.Equity_Stock).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.OpeningBalanceEquity, accounts.Where(i => i.SubType == AccountSubTypes.Equity_OpeningBalance).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.DiscountGiven, accounts.Where(i => i.SubType == AccountSubTypes.Expense_DiscountGiven).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Income_DiscountTaken, accounts.Where(i => i.Type == AccountTypes.Income && i.SubType == AccountSubTypes.Income_DiscountTaken).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Inventory, accounts.Where(i => i.SubType == AccountSubTypes.Asset_Inventory).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Asset_AccountReceivable, accounts.Where(i => i.SubType == AccountSubTypes.Asset_AccountReceivable).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EarnestAsset, accounts.Where(i => i.SubType == AccountSubTypes.Asset_EarnestPayment).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EarnestLiability, accounts.Where(i => i.SubType == AccountSubTypes.EarnestReceive).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Expense_BankFee, accounts.Where(i => i.SubType == AccountSubTypes.Expense_BankFee).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EquityPremiumStock, accounts.Where(i => i.SubType == AccountSubTypes.Equity_OverStockValue).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.CostOfGoodSold, accounts.Where(i => i.SubType == AccountSubTypes.Expense_CostOfGoodsSold).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.OverRecivePayment, accounts.Where(i => i.SubType == AccountSubTypes.OverReceivePayment).FirstOrDefault());



            this.erpNodeDBContext.SaveChanges();
            Console.WriteLine("  => Complete");
        }

        public void AutoCreateSystemAccount()
        {
            var systemAccountTypeList = Enum.GetValues(typeof(DefaultAccountType)).Cast<DefaultAccountType>();

            systemAccountTypeList.ToList().ForEach(t =>
            {
                this.Create(t);
            });
        }
    }
}