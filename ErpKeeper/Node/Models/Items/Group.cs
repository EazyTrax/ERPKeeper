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
        public Guid? ParentUid { get; set; }
        [ForeignKey("ParentUid")]
        public virtual Item Parent { get; set; }
        public virtual ICollection<Item> Child { get; set; }
     
    }
}