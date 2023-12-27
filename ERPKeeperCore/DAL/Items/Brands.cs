
using KeeperCore.ERPNode.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class Brands : ERPNodeDalRepository
    {
        public Brands(Organization organization) : base(organization)
        {

        }

        public List<Brand> ListAll() => erpNodeDBContext.Brands.ToList();
        public List<Brand> ListOnline() => erpNodeDBContext.Brands.Where(b => b.PublishOnline).ToList();
        public Brand Find(Guid id) => erpNodeDBContext.Brands.Find(id);

        public void Delete(Guid id)
        {
            var brand = erpNodeDBContext.Brands.Find(id);

            erpNodeDBContext.Brands.Remove(brand);
            organization.SaveChanges();
        }

        public Brand CreateNew(Brand brand)
        {
            brand.Id = Guid.NewGuid();
            erpNodeDBContext.Brands.Add(brand);
            return brand;
        }
        public Brand CreateNew(string name)
        {
            Brand brand = new()
            {
                Name = name
            };


            erpNodeDBContext.Brands.Add(brand);
            erpNodeDBContext.SaveChanges();
            return brand;
        }
    }
}
