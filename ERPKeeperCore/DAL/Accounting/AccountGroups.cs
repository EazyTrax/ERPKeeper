
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.Enums;
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

        public Account Find(Guid AccountId)
        {
            return erpNodeDBContext.AccountItems
                .Where(accountItem => accountItem.IsFolder == true && accountItem.Id == AccountId)
                .FirstOrDefault();
        }

        public Account Find(string accountGroupId)
        {
            return erpNodeDBContext.AccountItems.Find(Guid.Parse(accountGroupId));
        }

        public Account Create(AccountTypes query)
        {
            Account accountItemModel = new Account()
            {
                Type = query,
                IsFolder = true
            };

            return accountItemModel;
        }

        public List<Account> Get(AccountTypes AccountType)
        {
            return erpNodeDBContext.AccountItems
            .Where(account => account.Type == AccountType)
            .ToList();
        }



        public Account Save(Account accountGroup)
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

