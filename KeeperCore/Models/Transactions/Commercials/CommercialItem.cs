using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace KeeperCore.ERPNode.Models.Transactions
{

    [Table("ERP_Transactions_Commercial_Items")]
    public class CommercialItem
    {
        [Key]
        public Guid Id { get; set; }


        
        [Column("ItemId")]
        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual KeeperCore.ERPNode.Models.Items.Item Item { get; set; }

        public Accounting.Enums.ERPObjectType TransactionType { get; set; }

        public Guid? ItemBrandId { get; set; }
        public String ItemPartNumber { get; set; }
        public String ItemDescription { get; set; }

        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Commercial Commercial { get; set; }

        public int Order { get; set; }
        [Column("UnitPrice")]
        public Decimal UnitPrice { get; set; }

        public Decimal DiscountPercent { get; set; }
        public Decimal UnitPriceAfterDiscount => this.UnitPrice * (((decimal)100 - DiscountPercent) / (decimal)100);


        public int Amount { get; set; }
        public string Memo { get; set; }
        public string FIFOCostingMemo { get; set; }
        public string SerialNumber { get; set; }
        public string ExportSerialNumber { get; set; }

        public void UpdateExportSerialNumber()
        {
            if (!String.IsNullOrWhiteSpace(SerialNumber))
                ExportSerialNumber = "Serial Number:\n" + (SerialNumber?.Replace("\n", ", "))?.Trim();
        }




        [DisplayFormat(DataFormatString = "N2")]
        [DefaultValue(0)]
        public Decimal LineTotal => Math.Round(this.UnitPriceAfterDiscount * (decimal)this.Amount, 2, MidpointRounding.ToEven);






        //COSG Section 
        public int InputAmount { get; set; }
        public decimal InputValue => this.UnitPrice * InputAmount;
        public int OutputAmount { get; set; }



        public void Update(CommercialItem commercialItem)
        {
            if (commercialItem == null)
                return;

            this.Amount = commercialItem.Amount;
            this.ItemDescription = commercialItem.ItemDescription;
            this.ItemPartNumber = commercialItem.ItemPartNumber;
            this.UnitPrice = commercialItem.UnitPrice;
            this.DiscountPercent = commercialItem.DiscountPercent;
            this.Order = commercialItem.Order;
            this.SerialNumber = commercialItem.SerialNumber;
            this.Memo = commercialItem.Memo;

            this.UpdateInventory();
        }

        public void UpdateInventory()
        {
            if (this.Item.ItemType != Items.Enums.ItemTypes.Inventory)
                return;

            switch (this.Commercial.TransactionType)
            {
                case Models.Accounting.Enums.ERPObjectType.Sale:
                case Models.Accounting.Enums.ERPObjectType.PurchaseReturn:
                    this.InputAmount = 0;
                    this.OutputAmount = this.Amount;
                    break;

                case Models.Accounting.Enums.ERPObjectType.Purchase:
                case Models.Accounting.Enums.ERPObjectType.SalesReturn:
                default:
                    this.InputAmount = this.Amount;
                    this.OutputAmount = 0;
                    break;
            }
        }

        public CommercialItem()
        {
            
        }
    }
}
