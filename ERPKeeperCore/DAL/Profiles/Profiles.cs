using KeeperCore.ERPNode.Models.Profiles;
using KeeperCore.ERPNode.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace KeeperCore.ERPNode.DAL.Profiles
{
    public class Profiles : ERPNodeDalRepository
    {
        public IQueryable<Profile> GetQuery() => erpNodeDBContext.Profiles;
        public List<Profile> GetCustomers => erpNodeDBContext.Customers.Select(r => r.Profile).ToList();
        public List<Profile> GetSuppliers => erpNodeDBContext.Suppliers.Select(r => r.Profile).ToList();
        public List<Profile> GetPermissionProfileList => erpNodeDBContext.Profiles.Where(p => p.AccessLevel != AccessLevel.None).ToList();
        public Profile GetSelfOrganization => erpNodeDBContext.Profiles.Where(r => r.IsSelfOrganization == true).First();

        public Profile SearchByTaxNumber(string TaxNumber, bool createIfNotExist)
        {
            var existProfile = erpNodeDBContext.Profiles.Where(p => p.TaxNumber == TaxNumber).FirstOrDefault();

                return existProfile;
       
        }

       


        public Profile Authen(LogInModel loginModel)
        {
            if (loginModel.Email == null)
                return null;

            return this.GetQuery()
            .Where(p => p.Email.ToLower() == loginModel.Email || p.TaxNumber.ToLower() == loginModel.Email)
            .Where(p => p.Pin == loginModel.Pin && p.Pin != "")
            .Where(p => p.AccessLevel != AccessLevel.None)
            .FirstOrDefault();
        }


        public object ListHistoryEvent(Guid id)
        {
            var profileHistories = erpNodeDBContext.HistoryItems
                .Where(historyItem => historyItem.ProfileId == id)
                .GroupBy(historyItem => historyItem.KeyId)
                .Select(hi => hi.OrderByDescending(h => h.RecordDate).FirstOrDefault())
                .OrderByDescending(historyItem => historyItem.RecordDate)
                .Take(10)
                .ToList();

            return profileHistories;
        }

     

        public Profile Find(Guid id) => erpNodeDBContext.Profiles.Find(id);

     
        public void Remove(Profile profile)
        {
            Console.WriteLine("DELETE > " + profile.Name);

            if (profile.Supplier != null)
                erpNodeDBContext.Suppliers.Remove(profile.Supplier);
            if (profile.Customer != null)
                erpNodeDBContext.Customers.Remove(profile.Customer);
            if (profile.Employee != null)
                erpNodeDBContext.Employees.Remove(profile.Employee);
            if (profile.Investor != null)
                erpNodeDBContext.Investors.Remove(profile.Investor);



            erpNodeDBContext.ProfileBankAccounts.RemoveRange(profile.BankAccounts);
            erpNodeDBContext.ProfileAddresses.RemoveRange(profile.Addresses);
            erpNodeDBContext.HistoryItems.RemoveRange(profile.Histories);
            erpNodeDBContext.Profiles.Remove(profile);
            erpNodeDBContext.SaveChanges();
        }
   
        public Profile Get(Guid Id) => erpNodeDBContext.Profiles.Find(Id);
        public Profiles(Organization organization) : base(organization)
        {

        }

        public IQueryable<Profile> GetProfilesList(ProfileType type)
        {
            switch (type)
            {
                case ProfileType.People:
                    return this.People;
                case ProfileType.Organization:
                    return this.Organization;
                default:
                    return erpNodeDBContext.Profiles;
            }
        }


        public IQueryable<Profile> GetProfiles(ProfileViewType type)
        {
            switch (type)
            {
                case ProfileViewType.People:
                    return this.People;
                case ProfileViewType.Organization:
                    return this.Organization;
                //case ProfileViewType.Active:
                //    return this.Organization;
                //case ProfileViewType.InActive:
                //    return this.Organization;
                default:
                    return erpNodeDBContext.Profiles;
            }
        }




        public IQueryable<Profile> Organization => erpNodeDBContext.Profiles.Where(p => p.ProfileType == ProfileType.Organization);
        public IQueryable<Profile> People => erpNodeDBContext.Profiles.Where(p => p.ProfileType == ProfileType.People);

        //public IQueryable<Profile> GetProfileListByStatus(bool status) => erpNodeDBContext.Profiles.Where(p => p.IsActive =);


        public IQueryable<Profile> RdQueryable => Organization.Where(p => p.TaxNumber != null);
        public IList<Profile> ListAll() => erpNodeDBContext.Profiles.ToList();
        public Profile Save(Profile profile)
        {
            var existingProfile = erpNodeDBContext.Profiles.Find(profile.Id);
            existingProfile.Update(profile);
            erpNodeDBContext.SaveChanges();

            return existingProfile;

        }
        public Profile ChangePin(Profile profile)
        {
            var existingProfile = erpNodeDBContext.Profiles.Find(profile.Id);
            erpNodeDBContext.SaveChanges();

            return existingProfile;
        }
        public Profile CreateNew(Models.Profiles.ProfileType type, string name, string email, string taxId)
        {

            var profile = new KeeperCore.ERPNode.Models.Profiles.Profile()
            {
                Id = Guid.NewGuid(),
                ProfileType = type,
                Name = name,
                TaxNumber = taxId,
                Email = email,
                CreatedDate = DateTime.Now,
                localizedLanguage = KeeperCore.ERPNode.Models.Profiles.EnumLanguage.en
            };

            erpNodeDBContext.Profiles.Add(profile);
            erpNodeDBContext.SaveChanges();

            return profile;
        }
        public Profile CreateNew(ProfileType Type)
        {
            var newProfile = new Profile()
            {
                ProfileType = Type,
                localizedLanguage = EnumLanguage.en,
                Id = Guid.NewGuid(),
                Name = "New Profile",
            };
            erpNodeDBContext.Profiles.Add(newProfile);
            erpNodeDBContext.SaveChanges();

            return newProfile;
        }

        public void RemoveUnReferenceProfile()
        {
            var profiles = erpNodeDBContext.Profiles.ToList();

            profiles.ForEach(p =>
            {
                int commercialCount = erpNodeDBContext.Commercials.Where(c => c.ProfileId == p.Id).Count();
                int estimateCount = erpNodeDBContext.Estimates.Where(c => c.ProfileId == p.Id).Count();
                int employeeCount = erpNodeDBContext.EmployeePayments.Where(c => c.EmployeeId == p.Id).Count();
                int capitalCount = erpNodeDBContext.CapitalActivities.Where(c => c.InvestorId == p.Id).Count();

                var total = commercialCount + estimateCount + employeeCount + capitalCount;


                if (total == 0 && !p.IsSelfOrganization)
                {
                    this.Remove(p);
                }
            });
            erpNodeDBContext.SaveChanges();
        }
    }
}