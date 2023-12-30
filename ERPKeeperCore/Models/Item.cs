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
    [Table("ERP_Items")]
    public partial class Item
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public ItemStatus Status { get; set; }      
        public Enums.ItemTypes ItemType { get; set; }
        public int Level { get; set; }
        public int TotalCount { get; set; }
     
        public void UpdateFromNewERP(Item model)
        {
            //General
            this.PartNumber = model.PartNumber;
            this.UPC = model.UPC;
            this.Description = model.Description;
            this.Status = model.Status;
            this.BrandId = model.BrandId;
            this.Specification = model.Specification;

            //Sales
            this.SalePrice = model.SalePrice;
            this.IncomeAccountId = model.IncomeAccountId;

            //Purchase
            this.PurchaseAccountId = model.PurchaseAccountId;
        }

        [MaxLength(30)]
        public String PartNumber { get; set; }

        [Column("UPC")]
        public String UPC { get; set; }

        public String Description { get; set; }
        public bool OnlineSale { get; set; }
        public int CommercialAmount { get; set; }


        public string GroupName { get; set; }


        [Column(TypeName = "Money")]
        public Decimal SalePrice { get; set; }
        public Decimal PurchasePrice { get; set; }
        public virtual ICollection<TransactionLine> CommercialItems { get; set; }
    


     


        public Guid? ItemGroupId { get; set; }
        [ForeignKey("ItemGroupId")]
        public virtual ItemGroup ItemGroup { get; set; }



         public string Specification { get; set; }
        public Guid? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        public String BrandName => Brand?.Name;

        public Guid? DefaultSupplyierId { get; set; }
        [ForeignKey("DefaultSupplyierId")]
        public virtual Profile DefaultSupplyier { get; set; }





        [DisplayName("Purchase Account")]
        public Guid? PurchaseAccountId { get; set; }
        [ForeignKey("PurchaseAccountId")]
        public virtual Account PurchaseAccount { get; set; }


        [DisplayName("Income Account")]
        public Guid? IncomeAccountId { get; set; }
        [ForeignKey("IncomeAccountId")]
        public virtual Account IncomeAccount { get; set; }


        public int StockAmount { get; set; }
         public DateTime? CreatedDate { get; set; }
    }
}