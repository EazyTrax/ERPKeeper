using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Items
{

    public partial class Item
    {

        public virtual ICollection<KitMember> KitItems { get; set; }


        public KitMember addKitChildItem(Guid itemId)
        {
            var kitItem = KitItems.Where(i => i.ParentId == itemId).FirstOrDefault();

            if (kitItem == null)
            {
                kitItem = new KitMember()
                {
                    Id = Guid.NewGuid(),
                    Amount = 1,
                    ItemId = itemId,
                    ParentId = Id
                };
                KitItems.Add(kitItem);
            }

            return kitItem;
        }
    }
}