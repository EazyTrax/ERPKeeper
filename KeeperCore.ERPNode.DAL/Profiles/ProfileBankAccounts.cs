﻿
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Profiles
{
    public class ProfileBankAccounts : ERPNodeDalRepository
    {
        public ProfileBankAccounts(Organization organization) : base(organization)
        {

        }

        public IQueryable<ProfileBankAccount> Query() => erpNodeDBContext.ProfileBankAccounts;
        public List<ProfileBankAccount> ListAll() => erpNodeDBContext.ProfileBankAccounts.ToList();
        public ProfileBankAccount Find(Guid id) => erpNodeDBContext.ProfileBankAccounts.Find(id);

        public void Delete(Guid id)
        {
            var profileAddress = erpNodeDBContext.ProfileBankAccounts.Find(id);

            erpNodeDBContext.ProfileBankAccounts.Remove(profileAddress);
            organization.SaveChanges();
        }





        public ProfileBankAccount CreateNew(Guid profileId)
        {
            var bankAccount = new ProfileBankAccount()
            {
                ProfileId = profileId,
                BankAccountId = Guid.NewGuid()
            };


            erpNodeDBContext.ProfileBankAccounts.Add(bankAccount);
            erpNodeDBContext.SaveChanges();

            return bankAccount;
        }
        public ProfileBankAccount CreateNew(ProfileBankAccount profileAddress)
        {
            profileAddress.BankAccountId = Guid.NewGuid();
            erpNodeDBContext.ProfileBankAccounts.Add(profileAddress);
            return profileAddress;
        }
        public ProfileBankAccount CreateNew(string name)
        {
            ProfileBankAccount profileAddress = new()
            {
                Name = name,
            }; 

            erpNodeDBContext.ProfileBankAccounts.Add(profileAddress);
            erpNodeDBContext.SaveChanges();
            return profileAddress;
        }
    }
}
