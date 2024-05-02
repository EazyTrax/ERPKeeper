
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Items
{
    public class ItemGroup
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual ItemGroup Parent { get; set; }

        public String? Name { get; set; }

        public String? Detail { get; set; }
        public int ItemsCount { get; set; }



        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
        public virtual ICollection<ItemGroup> Child { get; set; } = new HashSet<ItemGroup>();


        public void Refresh()
        {

            this.ItemsCount = Items.Count;
        }
    }
}