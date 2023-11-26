

using ERPKeeper.Node.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeper.Node.DAL.Items
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
            itemImage.Uid = Guid.NewGuid();
            erpNodeDBContext.ItemImages.Add(itemImage);
            return itemImage;
        }
        public ItemImage CreateNew(string name)
        {
            var itemImage = erpNodeDBContext.ItemImages.Create();
            itemImage.Uid = Guid.NewGuid();

            erpNodeDBContext.ItemImages.Add(itemImage);
            erpNodeDBContext.SaveChanges();
            return itemImage;
        }
    }
}
