
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Accounting
{
    public class AccountGroups : ERPNodeDalRepository
    {
        public AccountGroups(Organization organization) : base(organization)
        {

        }

        public AccountItem Find(Guid AccountId)
        {
            return erpNodeDBContext.AccountItems
                .Where(accountItem => accountItem.IsFolder == true && accountItem.Id == AccountId)
                .FirstOrDefault();
        }

        public AccountItem Find(string accountGroupId)
        {
            return erpNodeDBContext.AccountItems.Find(Guid.Parse(accountGroupId));
        }

        public AccountItem Create(AccountTypes query)
        {
            AccountItem accountItemModel = new AccountItem()
            {
                Type = query,
                IsFolder = true
            };

            return accountItemModel;
        }

        public List<AccountItem> Get(AccountTypes AccountType)
        {
            return erpNodeDBContext.AccountItems
            .Where(account => account.Type == AccountType)
            .ToList();
        }



        public AccountItem Save(AccountItem accountGroup)
        {
            var exist = erpNodeDBContext.AccountItems.Find(accountGroup.Id);

            if (exist == null)
            {
                accountGroup.Id = Guid.NewGuid();
                accountGroup.IsFolder = true;

                erpNodeDBContext.AccountItems.Add(accountGroup);
                exist = accountGroup;
            }
            else
            {
                exist.Name = accountGroup.Name;
                exist.ParentId = accountGroup.ParentId;
                exist.CodeName = accountGroup.CodeName;
            }

            erpNodeDBContext.SaveChanges();
            return exist;
        }

    }
}

