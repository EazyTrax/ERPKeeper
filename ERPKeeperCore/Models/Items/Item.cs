﻿using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Items
{
    public partial class Item
    {
        [Key]
        public Guid Id { get; set; }
        public ItemStatus Status { get; set; }
        public ItemTypes ItemType { get; set; }


        [MaxLength(64)]
        public String? PartNumber { get; set; }
        public String? UPC { get; set; }

        public String? Description { get; set; }
        public bool OnlineSale { get; set; }
        public Decimal SalePrice { get; set; }
        public Decimal PurchasePrice { get; set; }


        public Guid? ItemGroupId { get; set; }
        [ForeignKey("ItemGroupId")]
        public virtual ItemGroup ItemGroup { get; set; }

        public String? Specification { get; set; }

        public Guid? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand? Brand { get; set; }


        public Guid? PurchaseAccountId { get; set; }
        [ForeignKey("PurchaseAccountId")]
        public virtual Account? PurchaseAccount { get; set; }


        [DisplayName("IncomeAccount")]
        public Guid? IncomeAccountId { get; set; }
        [ForeignKey("IncomeAccountId")]
        public virtual Account? IncomeAccount { get; set; }


        public int StockAmount { get; set; }
        public DateTime CreatedDate { get; set; }


        public String? BrandName { get; set; }
        public Guid GroupItemId { get; set; }
        public int AmountPurchase { get; set; }
        public int AmountOnHand { get; set; }
        public int AmountSold { get; set; }
        public int AmountOnPurchaseOrder { get; set; }
        public int AmountOnQuotation { get; set; }
        public int AmountOnSaleOrder { get; set; }
    }
}