
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Investors;
using ERPKeeperCore.Enterprise.Models.Profiles;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Investors : ERPNodeDalRepository
    {
        public Investors(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Investor> GetAll()
        {
            return erpNodeDBContext.Investors.ToList();
        }

        public int Count() => erpNodeDBContext.Investors.Count();

        public Investor? Find(Guid Id) => erpNodeDBContext.Investors.Find(Id);
        public void Add(Profile profile)
        {
            var Investor = new Investor
            {
                Id = profile.Id,
                Profile = profile,
                Code = profile.ShotName,
                Status =  Models.ProfileStatus.Active,
            };

            erpNodeDBContext.Investors.Add(Investor);
            erpNodeDBContext.SaveChanges();
        }

    }
}