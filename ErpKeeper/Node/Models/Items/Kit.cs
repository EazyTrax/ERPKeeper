using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Items
{

    public partial class Item
    {

        public virtual ICollection<KitMember> KitItems { get; set; }


        public KitMember addKitChildItem(Guid itemId)
        {
            var kitItem = KitItems.Where(i => i.ParentGuid == itemId).FirstOrDefault();

            if (kitItem == null)
            {
                kitItem = new KitMember()
                {
                    Uid = Guid.NewGuid(),
                    Amount = 1,
                    ItemGuid = itemId,
                    ParentGuid = Uid
                };
                KitItems.Add(kitItem);
            }

            return kitItem;
        }
    }
}