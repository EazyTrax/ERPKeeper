using KeeperCore.ERPNode.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Items
{
    public partial class Item
    {
        [Key]
        public Guid Id { get; set; }
        public ItemStatus Status { get; set; }
        public ItemTypes ItemType { get; set; }


        [MaxLength(64)]
        public String PartNumber { get; set; }

        [Column("UPC")]
        public String UPC { get; set; }

        public String Description { get; set; }

        public bool OnlineSale { get; set; }


        public Decimal SalePrice { get; set; }
        public Decimal PurchasePrice { get; set; }


        public Guid? ItemGroupId { get; set; }
        [ForeignKey("ItemGroupId")]
        public virtual ItemGroup ItemGroup { get; set; }

        public string Specification { get; set; }

        public Guid? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }


        public Guid? PurchaseAccountId { get; set; }
        [ForeignKey("PurchaseAccountId")]
        public virtual Account PurchaseAccount { get; set; }


        [DisplayName("IncomeAccount")]
        public Guid? IncomeAccountId { get; set; }
        [ForeignKey("IncomeAccountId")]
        public virtual Account IncomeAccount { get; set; }


        public int StockAmount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}