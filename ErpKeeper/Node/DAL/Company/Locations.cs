
using ERPKeeper.Node.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeper.Node.DAL.Company
{
    public class Locations : ERPNodeDalRepository
    {
        public Locations(Organization organization) : base(organization)
        {

        }

        public IList<Location> GetAll() => erpNodeDBContext.Locations.ToList();


        public IList<Location> GetRootList() => erpNodeDBContext.Locations.Where(l => l.ParentUid == null).ToList();
        public Location Find(Guid id) => erpNodeDBContext.Locations.Find(id);

        public void Remove(Location customer)
        {
            erpNodeDBContext.Locations.Remove(customer);
            erpNodeDBContext.SaveChanges();
        }

        public Location Create(Location newLocation)
        {

            erpNodeDBContext.Locations.Add(newLocation);
            erpNodeDBContext.SaveChanges();
            return newLocation;
        }




    }
}