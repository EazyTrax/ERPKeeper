
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Profiles;
using KeeperCore.ERPNode.Models.Suppliers;
using KeeperCore.ERPNode.Models.Suppliers.Enums;

namespace KeeperCore.ERPNode.DAL.Profiles
{
    public class Suppliers : ERPNodeDalRepository
    {
        public Suppliers(Organization organization) : base(organization)
        {

        }

        public IQueryable<Models.Profiles.Profile> All
        {
            get
            {
                return erpNodeDBContext.Suppliers
                   .Select(r => r.Profile);
            }
        }
        public List<Supplier> GetAll()
        {
            return erpNodeDBContext.Suppliers.ToList();
        }

        public IQueryable<Models.Suppliers.Supplier> GetByType(ProfileQueryType Type = ProfileQueryType.Active)
        {
            switch (Type)
            {
                case ProfileQueryType.Active:
                    return erpNodeDBContext.Suppliers
                   .Where(s => s.Status == SupplierStatus.Active);

                case ProfileQueryType.InActive:
                    return erpNodeDBContext.Suppliers
                   .Where(s => s.Status == Models.Suppliers.Enums.SupplierStatus.InActive);

                case ProfileQueryType.All:
                default:
                    return erpNodeDBContext.Suppliers;
            }
        }

        public Supplier Find(Guid id) => erpNodeDBContext.Suppliers.Find(id);

        public Supplier Create(Profile model)
        {
            var existModel = erpNodeDBContext.Suppliers.Find(model.Id);

            if (existModel != null)
                return existModel;

            existModel = new Supplier()
            {
                ProfileId = model.Id,
                Status = SupplierStatus.Active,
            };

            erpNodeDBContext.Suppliers.Add(existModel);
            erpNodeDBContext.SaveChanges();
            return existModel;
        }

        public Supplier Search(string searchDocument)
        {
            var profile = erpNodeDBContext.Suppliers
                .Where(c => c.Profile.TaxNumber == searchDocument || c.Profile.ShotName == searchDocument)
                .FirstOrDefault();
            return profile;
        }
    }
}