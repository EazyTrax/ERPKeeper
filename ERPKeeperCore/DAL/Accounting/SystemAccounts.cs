using KeeperCore.ERPNode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models;
using KeeperCore.ERPNode.Models.Enums;

namespace KeeperCore.ERPNode.DAL.Accounting
{
    public class SystemAccounts : ERPNodeDalRepository
    {
        public SystemAccounts(Organization organization) : base(organization)
        {

        }

        public List<DefaultAccount> GetAll()
        {
            return erpNodeDBContext.SystemAccounts.ToList();
        }

        public Account Cash => this.GetAccount(DefaultAccountType.Cash);
        public Account BankFee => this.GetAccount(DefaultAccountType.BankFee);
        public Account COSG => this.GetAccount(DefaultAccountType.CostOfGoodSold);
        public Account OverPayment => this.GetAccount(DefaultAccountType.OverRecivePayment);
        public Account RetainedEarning => this.GetAccount(DefaultAccountType.RetainedEarning);
        public Account AccountPayable => this.GetAccount(DefaultAccountType.AccountPayable);
        public Account AccountReceivable => this.GetAccount(DefaultAccountType.AccountReceivable);
        public Account OpeningBalanceEquity => this.GetAccount(DefaultAccountType.OpeningBalanceEquity);

        public void Add(DefaultAccount defaultAccountItem)
        {
            erpNodeDBContext.SystemAccounts.Add(defaultAccountItem);
            erpNodeDBContext.SaveChanges();
        }

        public Account EquityStock => this.GetAccount(DefaultAccountType.EquityStock);
        public Account DiscountGiven => this.GetAccount(DefaultAccountType.DiscountGiven);
        public Account DiscountTaken => this.GetAccount(DefaultAccountType.DiscountTaken);
        public Account Income => this.GetAccount(DefaultAccountType.Income);
        public Account Expense => this.GetAccount(DefaultAccountType.Expense);

        public Account GetAccount(DefaultAccountType type)
        {
            var defaultAccountItem = erpNodeDBContext.SystemAccounts.Find(type);
            if (defaultAccountItem != null)
                return defaultAccountItem.Account;
            else
                return null;
        }

        public DefaultAccount Find(DefaultAccountType type) => erpNodeDBContext.SystemAccounts.Find(type);



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
                var defaultAccountItem = erpNodeDBContext.SystemAccounts.Find(defaultAccountType);

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
                    erpNodeDBContext.SystemAccounts.Add(defaultAccountItem);
                }
            }
            erpNodeDBContext.SaveChanges();

        }

        public void Create(DefaultAccountType accountType)
        {
            var defaultAccountItem = erpNodeDBContext.SystemAccounts.Find(accountType);

            if (defaultAccountItem == null)
            {
                defaultAccountItem = new DefaultAccount()
                {
                    Type = accountType,
                    LastUpdate = DateTime.Now
                };

                erpNodeDBContext.SystemAccounts.Add(defaultAccountItem);
                erpNodeDBContext.SaveChanges();
            }
        }



        public void AutoAssignSystemAccount()
        {
            this.AutoCreateSystemAccount();

            Console.WriteLine("=> Auto Assign System Account");
            var accounts = erpNodeDBContext.AccountItems.Where(i => i.IsFolder == false).ToList();

            this.SetIfUnAssign(DefaultAccountType.Income, accounts.Where(i => i.Type == AccountTypes.Income).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Cash, accounts.Where(i => i.SubType == AccountSubTypes.Cash).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.AccountPayable, accounts.Where(i => i.SubType == AccountSubTypes.AccountPayable).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Expense, accounts.Where(i => i.SubType == AccountSubTypes.Expense).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.RetainedEarning, accounts.Where(i => i.SubType == AccountSubTypes.RetainEarning).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EquityStock, accounts.Where(i => i.SubType == AccountSubTypes.Stock).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.OpeningBalanceEquity, accounts.Where(i => i.SubType == AccountSubTypes.OpeningBalance).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.DiscountGiven, accounts.Where(i => i.SubType == AccountSubTypes.DiscountGiven).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.DiscountTaken, accounts.Where(i => i.Type == AccountTypes.Income && i.SubType == AccountSubTypes.DiscountTaken).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.Inventory, accounts.Where(i => i.SubType == AccountSubTypes.Inventory).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.AccountReceivable, accounts.Where(i => i.SubType == AccountSubTypes.AccountReceivable).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EarnestAsset, accounts.Where(i => i.SubType == AccountSubTypes.EarnestPayment).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EarnestLiability, accounts.Where(i => i.SubType == AccountSubTypes.EarnestReceive).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.BankFee, accounts.Where(i => i.SubType == AccountSubTypes.BankFee).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.EquityPremiumStock, accounts.Where(i => i.SubType == AccountSubTypes.OverStockValue).FirstOrDefault());
            this.SetIfUnAssign(DefaultAccountType.CostOfGoodSold, accounts.Where(i => i.SubType == AccountSubTypes.CostOfGoodsSold).FirstOrDefault());
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