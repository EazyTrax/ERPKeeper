

using KeeperCore.ERPNode.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class ItemImages : ERPNodeDalRepository
    {
        public ItemImages(Organization organization) : base(organization)
        {

        }

        public IQueryable<ItemImage> GetQuery()
        {
            return erpNodeDBContext.ItemImages;
        }

        public List<ItemImage> GetListAll()
        {
            return erpNodeDBContext.ItemImages.ToList();
        }

        public ItemImage Find(Guid id) => erpNodeDBContext.ItemImages.Find(id);

        public void Delete(Guid id)
        {
            var itemImage = erpNodeDBContext.ItemImages.Find(id);

            erpNodeDBContext.ItemImages.Remove(itemImage);
            organization.SaveChanges();
        }

        public ItemImage CreateNew(ItemImage itemImage)
        {
            itemImage.Id = Guid.NewGuid();
            erpNodeDBContext.ItemImages.Add(itemImage);
            return itemImage;
        }
        public ItemImage CreateNew(string name)
        {
            ItemImage itemImage = new()
            {
                Name = name
            };
  

            erpNodeDBContext.ItemImages.Add(itemImage);
            erpNodeDBContext.SaveChanges();
            return itemImage;
        }
    }
}
