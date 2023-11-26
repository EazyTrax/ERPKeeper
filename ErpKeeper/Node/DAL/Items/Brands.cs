
using DevExpress.Utils.Extensions;
using ERPKeeper.Node.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeper.Node.DAL.Items
{
    public class Brands : ERPNodeDalRepository
    {
        public Brands(Organization organization) : base(organization)
        {

        }

        public List<Brand> ListAll() => erpNodeDBContext.Brands.ToList();
        public List<Brand> ListOnline() => erpNodeDBContext.Brands.Where(b => b.PublishOnline).ToList();
       
        public Brand Find(Guid id) => erpNodeDBContext.Brands.Find(id);
        public Brand Find(string name) => erpNodeDBContext.Brands.Where(b => b.Name == name).FirstOrDefault();
        public void Delete(Guid id)
        {
            var brand = erpNodeDBContext.Brands.Find(id);

            erpNodeDBContext.Brands.Remove(brand);
            organization.SaveChanges();
        }

        public Brand CreateNew(Brand brand)
        {
            brand.Uid = Guid.NewGuid();
            erpNodeDBContext.Brands.Add(brand);
            return brand;
        }
        public Brand CreateNew(string name)
        {
            var brand = erpNodeDBContext.Brands.Create();
            brand.Uid = Guid.NewGuid();
            brand.Name = name;

            erpNodeDBContext.Brands.Add(brand);
            erpNodeDBContext.SaveChanges();
            return brand;
        }

        public void UpdateAmount()
        {
            erpNodeDBContext.Brands.ToList().ForEach(b => b.UpdateAmount());
            erpNodeDBContext.SaveChanges();
        }
    }
}
