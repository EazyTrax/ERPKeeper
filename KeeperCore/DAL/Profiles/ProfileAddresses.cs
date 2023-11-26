
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Profiles
{
    public class ProfileAddresses : ERPNodeDalRepository
    {
        public ProfileAddresses(Organization organization) : base(organization)
        {

        }

        public IQueryable<ProfileAddress> Query() => erpNodeDBContext.ProfileAddresses;
        public List<ProfileAddress> ListAll() => erpNodeDBContext.ProfileAddresses.ToList();
        public ProfileAddress Find(Guid id) => erpNodeDBContext.ProfileAddresses.Find(id);

        public void Delete(Guid id)
        {
            var profileAddress = erpNodeDBContext.ProfileAddresses.Find(id);

            erpNodeDBContext.ProfileAddresses.Remove(profileAddress);
            organization.SaveChanges();
        }
        public ProfileAddress CreateNew(Guid profileId)
        {
            var addreses = new ProfileAddress()
            {
                ProfileId = profileId,
                AddressId = Guid.NewGuid()
            };

            erpNodeDBContext.ProfileAddresses.Add(addreses);
            erpNodeDBContext.SaveChanges();

            return addreses;
        }
        public ProfileAddress CreateNew(ProfileAddress profileAddress)
        {
            profileAddress.AddressId = Guid.NewGuid();
            erpNodeDBContext.ProfileAddresses.Add(profileAddress);
            return profileAddress;
        }
        public ProfileAddress CreateNew(string name)
        {
            ProfileAddress profileAddress = new()
            {
                Name = name
            };
         

            erpNodeDBContext.ProfileAddresses.Add(profileAddress);
            erpNodeDBContext.SaveChanges();
            return profileAddress;
        }
    }
}
