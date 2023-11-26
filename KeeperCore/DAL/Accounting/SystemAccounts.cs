using KeeperCore.ERPNode.Models.Tasks;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.ChartOfAccount.Enums;

namespace KeeperCore.ERPNode.DAL.Accounting
{
    public class SystemAccounts : ERPNodeDalRepository
    {
        public SystemAccounts(Organization organization) : base(organization)
        {

        }

        public List<DefaultAccountItem> GetAll()
        {
            return erpNodeDBContext.SystemAccounts.ToList();
        }

        public AccountItem Cash => this.GetAccount(SystemAccountType.Cash);
        public AccountItem BankFee => this.GetAccount(SystemAccountType.BankFee);
        public AccountItem COSG => this.GetAccount(SystemAccountType.CostOfGoodSold);
        public AccountItem OverPayment => this.GetAccount(SystemAccountType.OverRecivePayment);
        public AccountItem RetainedEarning => this.GetAccount(SystemAccountType.RetainedEarning);
        public AccountItem AccountPayable => this.GetAccount(SystemAccountType.AccountPayable);
        public AccountItem AccountReceivable => this.GetAccount(SystemAccountType.AccountReceivable);
        public AccountItem OpeningBalanceEquity => this.GetAccount(SystemAccountType.OpeningBalanceEquity);

        public void Add(DefaultAccountItem defaultAccountItem)
        {
            erpNodeDBContext.SystemAccounts.Add(defaultAccountItem);
            erpNodeDBContext.SaveChanges();
        }

        public AccountItem EquityStock => this.GetAccount(SystemAccountType.EquityStock);
        public AccountItem DiscountGiven => this.GetAccount(SystemAccountType.DiscountGiven);
        public AccountItem DiscountTaken => this.GetAccount(SystemAccountType.DiscountTaken);
        public AccountItem Income => this.GetAccount(SystemAccountType.Income);
        public AccountItem Expense => this.GetAccount(SystemAccountType.Expense);

        public AccountItem GetAccount(SystemAccountType type)
        {
            var defaultAccountItem = erpNodeDBContext.SystemAccounts.Find(type);
            if (defaultAccountItem != null)
                return defaultAccountItem.AccountItem;
            else
                return null;
        }

        public DefaultAccountItem Find(SystemAccountType type) => erpNodeDBContext.SystemAccounts.Find(type);



        public void SetIfUnAssign(SystemAccountType defaultAccountType, AccountItem accountItem)
        {
            if (this.GetAccount(defaultAccountType) == null)
            {
                this.Set(defaultAccountType, accountItem);
            }
        }

        public void Set(SystemAccountType defaultAccountType, AccountItem accountItem)
        {
            if (accountItem != null)
            {
                var defaultAccountItem = erpNodeDBContext.SystemAccounts.Find(defaultAccountType);

                if (defaultAccountItem != null)
                {
                    defaultAccountItem.AccountItemId = accountItem.Id;
                    defaultAccountItem.AccountItem = accountItem;
                    defaultAccountItem.LastUpdate = DateTime.Now;
                }
                else
                {
                    defaultAccountItem = new DefaultAccountItem()
                    {
                        AccountType = defaultAccountType,
                        AccountItem = accountItem,
                        AccountItemId = accountItem.Id,
                        LastUpdate = DateTime.Now
                    };
                    erpNodeDBContext.SystemAccounts.Add(defaultAccountItem);
                }
            }
            erpNodeDBContext.SaveChanges();

        }

        public void Create(SystemAccountType accountType)
        {
            var defaultAccountItem = erpNodeDBContext.SystemAccounts.Find(accountType);

            if (defaultAccountItem == null)
            {
                defaultAccountItem = new DefaultAccountItem()
                {
                    AccountType = accountType,
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

            this.SetIfUnAssign(SystemAccountType.Income, accounts.Where(i => i.Type == AccountTypes.Income).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.Cash, accounts.Where(i => i.SubType == AccountSubTypes.Cash).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.AccountPayable, accounts.Where(i => i.SubType == AccountSubTypes.AccountPayable).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.Expense, accounts.Where(i => i.SubType == AccountSubTypes.Expense).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.RetainedEarning, accounts.Where(i => i.SubType == AccountSubTypes.RetainEarning).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.EquityStock, accounts.Where(i => i.SubType == AccountSubTypes.Stock).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.OpeningBalanceEquity, accounts.Where(i => i.SubType == AccountSubTypes.OpeningBalance).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.DiscountGiven, accounts.Where(i => i.SubType == AccountSubTypes.DiscountGiven).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.DiscountTaken, accounts.Where(i => i.Type == AccountTypes.Income && i.SubType == AccountSubTypes.DiscountTaken).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.Inventory, accounts.Where(i => i.SubType == AccountSubTypes.Inventory).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.AccountReceivable, accounts.Where(i => i.SubType == AccountSubTypes.AccountReceivable).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.EarnestAsset, accounts.Where(i => i.SubType == AccountSubTypes.EarnestPayment).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.EarnestLiability, accounts.Where(i => i.SubType == AccountSubTypes.EarnestReceive).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.BankFee, accounts.Where(i => i.SubType == AccountSubTypes.BankFee).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.EquityPremiumStock, accounts.Where(i => i.SubType == AccountSubTypes.OverStockValue).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.CostOfGoodSold, accounts.Where(i => i.SubType == AccountSubTypes.CostOfGoodsSold).FirstOrDefault());
            this.SetIfUnAssign(SystemAccountType.OverRecivePayment, accounts.Where(i => i.SubType == AccountSubTypes.OverReceivePayment).FirstOrDefault());



            this.erpNodeDBContext.SaveChanges();
            Console.WriteLine("  => Complete");
        }

        public void AutoCreateSystemAccount()
        {
            var systemAccountTypeList = Enum.GetValues(typeof(SystemAccountType)).Cast<SystemAccountType>();

            systemAccountTypeList.ToList().ForEach(t =>
            {
                this.Create(t);
            });
        }
    }
}