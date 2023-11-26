using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Items
{
    [Table("ERP_Item_Groups")]
    public class GroupItem
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }

        public String Name { get; set; }
        public String Detail { get; set; }

        public int ChildCount { get; set; }
        public int ItemsCount { get; set; }


        public Guid? ParentUid { get; set; }
        [ForeignKey("ParentUid")]
        public virtual GroupItem Parent { get; set; }


        public DateTime CreatedDate { get; internal set; }


        public virtual ICollection<GroupItem> Child { get; set; }
        public virtual ICollection<Item> Items { get; set; }












        public void SetParent(GroupItem parent)
        {
            this.ParentUid = parent.Uid;
        }

        public void UpdateGroupData(ItemGroup group)
        {
            this.Name = group.PartNumber;
            this.Detail = group.Description;
            this.ParentUid = group.ParentUid;
        }

        public bool IsChild(GroupItem item)
        {
            var isChild = false;
            foreach (var child in this.Child.ToList())
            {
                if (child.Uid == item.Uid)
                    return true;
                else
                    isChild = child.IsChild(item);
            };
            return isChild;
        }



    }
}