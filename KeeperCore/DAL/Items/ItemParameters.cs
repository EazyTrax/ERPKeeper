
using KeeperCore.ERPNode.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class ItemParameters : ERPNodeDalRepository
    {
        public ItemParameters(Organization organization) : base(organization)
        {

        }

        public IQueryable<ItemParameter> GetQuery() => erpNodeDBContext.ItemParameters;
        public List<ItemParameter> GetListAll() => erpNodeDBContext.ItemParameters.ToList();
        public ItemParameter Find(Guid id) => erpNodeDBContext.ItemParameters.Find(id);
        public void Delete(Guid id)
        {
            var itemParameter = erpNodeDBContext.ItemParameters.Find(id);

            erpNodeDBContext.ItemParameters.Remove(itemParameter);
            organization.SaveChanges();
        }
        public ItemParameter CreateNew(ItemParameter itemParameter)
        {
            itemParameter.Id = Guid.NewGuid();
            erpNodeDBContext.ItemParameters.Add(itemParameter);
            return itemParameter;
        }
        public ItemParameter CreateNew(string name)
        {
            var itemParameter = new ItemParameter()
            {
                Name = name
            };


            erpNodeDBContext.ItemParameters.Add(itemParameter);
            erpNodeDBContext.SaveChanges();
            return itemParameter;
        }
    }
}
