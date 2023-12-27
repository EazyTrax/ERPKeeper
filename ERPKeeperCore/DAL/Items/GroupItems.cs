using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.Items.Enums;
using KeeperCore.ERPNode.Models.Transactions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class GroupItems : ERPNodeDalRepository
    {
        public GroupItems(Organization organization) : base(organization)
        {

        }

        public IQueryable<GroupItem> GetAll() => erpNodeDBContext.GroupItems;
        public List<GroupItem> GetListAll() => GetAll().ToList();
        public List<GroupItem> GetRoot() => erpNodeDBContext
            .GroupItems
            .Where(ig => ig.ParentId == null)
            .ToList();

        public GroupItem Find(Guid id) => erpNodeDBContext.GroupItems.Find(id);
        public GroupItem Find(String name) => erpNodeDBContext.GroupItems.Where(g => g.Name == name).FirstOrDefault();
        public GroupItem Find(GroupItem group)
        {
            var exGroupItem = erpNodeDBContext.GroupItems.Find(group.Id);

            if (exGroupItem != null)
            {
                exGroupItem.Name = group.Name.Trim();
                exGroupItem.Detail = group.Detail.Trim();
                erpNodeDBContext.SaveChanges();
            }

            return exGroupItem;
        }

        public GroupItem FindOrCreate(string name, Guid? parentId = null)
        {
            if (this.Find(name) != null)
                return this.Find(name);
            else
            {
                var group = new GroupItem()
                {
                    Id = Guid.NewGuid(),
                    Name = name ?? "NA-Group",
                    ParentId = parentId,
                    CreatedDate = DateTime.Now,
                };

                erpNodeDBContext.GroupItems.Add(group);

                this.SaveChanges();
                return group;
            }
        }



        public void Delete(Guid id)
        {
            var itemGroup = erpNodeDBContext.GroupItems.Find(id);

            if (itemGroup.Parent != null)
            {
                itemGroup.Child.ToList().ForEach(c =>
                {
                    c.ParentId = itemGroup.Parent.Id;
                    c.Parent = itemGroup.Parent;
                });
            }
            else
            {
                itemGroup.Child.ToList().ForEach(c =>
                {
                    c.ParentId = null;
                    c.Parent = null;
                });
            }

            erpNodeDBContext.GroupItems.Remove(itemGroup);
            this.SaveChanges();
        }

     
    }
}
