using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ERPKeeper.Node.Models.Online
{

    [Table("ERP_ShoppingCart_Items")]
    public class ShoppingCartItem
    {
        [Key]
        public Guid Uid { get; set; }

        public string SessionId { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? ProfileUid { get; set; }
        [ForeignKey("ProfileUid")]
        public virtual ERPKeeper.Node.Models.Profiles.Profile Profile { get; set; }

        public Guid ItemGuid { get; set; }
        [ForeignKey("ItemGuid")]
        public virtual ERPKeeper.Node.Models.Items.Item Item { get; set; }



        [Column("UnitPrice")]
        public Decimal UnitPrice { get; set; }

        [DefaultValue(0)]
        public int Amount { get; set; }


        public string Memo { get; set; }

        [DisplayFormat(DataFormatString = "N2")]
        [NotMapped]
        public Decimal LineTotal => Amount * UnitPrice;

    }
}