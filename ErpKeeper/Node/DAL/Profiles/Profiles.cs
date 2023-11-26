using ERPKeeper.Node.Models.Profiles;
using ERPKeeper.Node.Models.Security;
using ERPKeeper.RdVat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ERPKeeper.Node.DAL.Profiles
{
    public class Profiles : ERPNodeDalRepository
    {
        public IQueryable<Profile> GetQuery() => erpNodeDBContext.Profiles;
        public List<Profile> GetCustomers => erpNodeDBContext.Customers.Select(r => r.Profile).ToList();
        public List<Profile> GetSuppliers => erpNodeDBContext.Suppliers.Select(r => r.Profile).ToList();
        public List<Profile> GetPermissionProfileList => erpNodeDBContext.Profiles.Where(p => p.AccessLevel != Models.Security.Enums.AccessLevel.None).ToList();
        public Profile GetSelfOrganization => erpNodeDBContext.Profiles.Where(r => r.IsSelfOrganization == true).First();

        public Profile SearchByTaxNumber(string TaxNumber, bool createIfNotExist)
        {
            var existProfile = erpNodeDBContext.Profiles.Where(p => p.TaxNumber == TaxNumber).FirstOrDefault();

            if (existProfile != null)
            {
                return existProfile;
            }
            else
            {
                return SearchRd(TaxNumber, createIfNotExist);
            }
        }

       

        public void SyncAllWithRd()
        {
            this.RdQueryable.ToList().ForEach(profile =>
            {
                this.SyncWithRd(profile);
            });
        }

        public Profile Authen(LogInModel loginModel)
        {
            if (loginModel.Email == null)
                return null;

            return this.GetQuery()
            .Where(p => p.Email.ToLower() == loginModel.Email || p.TaxNumber.ToLower() == loginModel.Email)
            .Where(p => p.Pin == loginModel.Pin && p.Pin != "")
            .Where(p => p.AccessLevel != Models.Security.Enums.AccessLevel.None)
            .FirstOrDefault();
        }

        public void SyncWithRd(Profile profile)
        {
            if (profile.TaxNumber != null)
            {
                profile.TaxNumber = profile.TaxNumber.Replace(" ", "");



                var myBinding = new BasicHttpBinding();
                myBinding.Security.Mode = BasicHttpSecurityMode.Transport;

                var myEndpointAddress = new EndpointAddress("https://rdws.rd.go.th/serviceRD3/vatserviceRD3.asmx");
                var RdClient = new RdVat.vatserviceRD3SoapClient(myBinding, myEndpointAddress);
                var result = RdClient.Service("anonymous", "anonymous", profile.TaxNumber, null, 0, 0, 0);

                if (result != null && result.vBranchNumber.Count == 1)
                {
                    profile.Name = result.vName[0].ToString();
                    profile.IsRDVerify = true;
                }

                erpNodeDBContext.SaveChanges();
            }
        }

        public object ListHistoryEvent(Guid id)
        {
            var profileHistories = erpNodeDBContext.HistoryItems
                .Where(historyItem => historyItem.ProfileGuid == id)
                .GroupBy(historyItem => historyItem.KeyUid)
                .Select(hi => hi.OrderByDescending(h => h.RecordDate).FirstOrDefault())
                .OrderByDescending(historyItem => historyItem.RecordDate)
                .Take(10)
                .ToList();

            return profileHistories;
        }

        public Profile SearchRd(string taxNumber, bool createIfNotExist)
        {
            var myBinding = new BasicHttpBinding();
            myBinding.Security.Mode = BasicHttpSecurityMode.Transport;

            var myEndpointAddress = new EndpointAddress("https://rdws.rd.go.th/serviceRD3/vatserviceRD3.asmx");
            var RdClient = new RdVat.vatserviceRD3SoapClient(myBinding, myEndpointAddress);
            var result = RdClient.Service("anonymous", "anonymous", taxNumber, null, 0, 0, 0);


            if (result.vBranchNumber.Count == 1)
            {
                var newProfile = new Profile()
                {
                    Uid = Guid.NewGuid(),
                    Name = result.vtitleName[0].ToString() + " " + result.vName[0].ToString(),
                    TaxNumber = taxNumber,
                    ProfileType = ProfileType.Organization,
                    IsRDVerify = true
                };

                if (createIfNotExist)
                {
                    erpNodeDBContext.Profiles.Add(newProfile);
                    erpNodeDBContext.SaveChanges();
                }

                return newProfile;
            }

            return null;
        }

        public Profile Find(Guid id) => erpNodeDBContext.Profiles.Find(id);

        public void UpdateRdAddresses(Guid ProfileGuid)
        {
            var existProfile = erpNodeDBContext.Profiles.Find(ProfileGuid);

            var myBinding = new BasicHttpBinding();
            myBinding.Security.Mode = BasicHttpSecurityMode.Transport;
            myBinding.MaxReceivedMessageSize = 2147483647;
            myBinding.MaxBufferSize = 2147483647;
            var myEndpointAddress = new EndpointAddress("https://rdws.rd.go.th/serviceRD3/vatserviceRD3.asmx");

            var RdClient = new RdVat.vatserviceRD3SoapClient(myBinding, myEndpointAddress);

            var Tins = new RdVat.ArrayOfString
            {
                existProfile.TaxNumber
            };

            var RDresult = RdClient.ServiceArr("anonymous", "anonymous", Tins);

            if (RDresult != null)
            {
                UpdateRdBranchAddress(existProfile, RDresult);
            }
        }
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
        private void UpdateRdBranchAddress(Profile existProfile, vat result)
        {
            var BranchCount = result.vBranchNumber.Count;

            for (int i = 0; i < BranchCount; i++)
            {
                string rdBranchNumber = result.vBranchNumber[i].ToString().PadLeft(5, '0');
                ProfileAddress profileAddress = existProfile.Addresses.Where(a => a.Number == rdBranchNumber).FirstOrDefault();

                if (profileAddress == null)
                {
                    profileAddress = new ProfileAddress
                    {
                        AddressGuid = Guid.NewGuid()
                    };
                    existProfile.Addresses.Add(profileAddress);
                }

                string addressLine = "";
                addressLine += result.vBranchTitleName[i].ToString() + " ";
                addressLine += result.vBranchName[i].ToString() + " ";
                addressLine += result.vBuildingName[i].ToString() + " ";
                addressLine += result.vRoomNumber[i].ToString() + " ";
                addressLine += result.vFloorNumber[i].ToString() + " ";
                addressLine += result.vVillageName[i].ToString() + " ";
                addressLine += result.vHouseNumber[i].ToString() + " ";
                addressLine += result.vMooNumber[i].ToString() + " ";
                addressLine += result.vSoiName[i].ToString() + " ";
                addressLine += result.vStreetName[i].ToString() + " ";
                addressLine += result.vThambol[i].ToString() + " ";
                addressLine += result.vAmphur[i].ToString() + " ";
                addressLine += result.vProvince[i].ToString() + " ";
                addressLine += result.vPostCode[i].ToString() + " ";


                profileAddress.Number = result.vBranchNumber[i].ToString().PadLeft(5, '0');
                profileAddress.Title = result.vBranchNumber[i].ToString();
                profileAddress.AddressLine = addressLine;
                profileAddress.RecordDate = DateTime.Now;

                if (profileAddress.Number == "00000")
                    profileAddress.IsPrimary = true;

            }

            erpNodeDBContext.SaveChanges();


        }
        public Profile Get(Guid Uid) => erpNodeDBContext.Profiles.Find(Uid);
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
            var existingProfile = erpNodeDBContext.Profiles.Find(profile.Uid);
            existingProfile.Update(profile);
            erpNodeDBContext.SaveChanges();

            return existingProfile;

        }
        public Profile ChangePin(Profile profile)
        {
            var existingProfile = erpNodeDBContext.Profiles.Find(profile.Uid);
            erpNodeDBContext.SaveChanges();

            return existingProfile;
        }
        public Profile CreateNew(Models.Profiles.ProfileType type, string name, string email, string taxId)
        {

            var profile = new ERPKeeper.Node.Models.Profiles.Profile()
            {
                Uid = Guid.NewGuid(),
                ProfileType = type,
                Name = name,
                TaxNumber = taxId,
                Email = email,
                CreatedDate = DateTime.Now,
                localizedLanguage = ERPKeeper.Node.Models.Profiles.EnumLanguage.en
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
                Uid = Guid.NewGuid(),
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
                int commercialCount = erpNodeDBContext.Commercials.Where(c => c.ProfileGuid == p.Uid).Count();
                int estimateCount = erpNodeDBContext.Estimates.Where(c => c.ProfileGuid == p.Uid).Count();
                int employeeCount = erpNodeDBContext.EmployeePayments.Where(c => c.EmployeeUid == p.Uid).Count();
                int capitalCount = erpNodeDBContext.CapitalActivities.Where(c => c.InvestorUid == p.Uid).Count();

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