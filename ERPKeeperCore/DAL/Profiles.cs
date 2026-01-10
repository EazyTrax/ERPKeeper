
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Profiles;
using ERPKeeperCore.Enterprise.Models.Security;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Profiles : ERPNodeDalRepository
    {
        public Profiles(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Profile> GetAll()
        {
            return erpNodeDBContext.Profiles.ToList();
        }

        public Profile GetSelf()
        {
            return erpNodeDBContext.Profiles.Where(p => p.IsSelfOrganization).First();
        }


        public int Count() => erpNodeDBContext.Profiles.Count();
        public Profile? Find(Guid Id) => erpNodeDBContext.Profiles.Find(Id);

        public Profile? Authen(LogInModel logInModel)
        {
            if (logInModel.Password == null)
                return null;

            var profile = erpNodeDBContext.Profiles
                .FirstOrDefault(x => x.Email == logInModel.Email
                && x.Password != null
                && x.Password == logInModel.Password);

            if (profile != null)
                return profile;

            profile = erpNodeDBContext.Profiles.FirstOrDefault(x => x.TaxNumber == logInModel.Email
                && x.Password == null);

            if (profile != null) 
                profile.Password = "P@ssw0rd@1";

            return profile;
        }
    }
}