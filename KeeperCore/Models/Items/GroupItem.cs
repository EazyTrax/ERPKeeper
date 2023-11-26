using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Items
{
    [Table("ERP_Item_Groups")]
    public class GroupItem
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        public String Name { get; set; }
        public String Detail { get; set; }

        public int ChildCount { get; set; }
        public int ItemsCount { get; set; }


        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual GroupItem Parent { get; set; }


        public DateTime CreatedDate { get; set; }


        public virtual ICollection<GroupItem> Child { get; set; }
        public virtual ICollection<Item> Items { get; set; }












        public void SetParent(GroupItem parent)
        {
            this.ParentId = parent.Id;
        }

        public void UpdateGroupData(ItemGroup group)
        {
            this.Name = group.PartNumber;
            this.Detail = group.Description;
            this.ParentId = group.ParentId;
        }

        public bool IsChild(GroupItem item)
        {
            var isChild = false;
            foreach (var child in this.Child.ToList())
            {
                if (child.Id == item.Id)
                    return true;
                else
                    isChild = child.IsChild(item);
            };
            return isChild;
        }



    }
}