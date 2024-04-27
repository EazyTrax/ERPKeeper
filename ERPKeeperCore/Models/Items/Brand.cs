
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models.Items
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }
        public String? ShotName { get; set; }
        public String? Name { get; set; }
        public String? WebSite { get; set; }
        public String? Description { get; set; }
        public bool PublishOnline { get; set; }
        public virtual int ItemsCount => this.Items?.Count ?? 0;
        public virtual int OnlineItemsCount => this.Items?.Where(i => i.OnlineSale).Count() ?? 0;
        public virtual ICollection<Item> Items { get; set; }

        public void Update(Brand brand)
        {
            this.Name = brand.Name;
            this.WebSite = brand.WebSite;
            this.Description = brand.Description;
            this.PublishOnline = brand.PublishOnline;
        }
    }
}