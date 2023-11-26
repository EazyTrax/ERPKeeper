using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Items
{
    /// <summary>
    /// Inventory Part
    /// </summary>



    public partial class Item
    {

        [DefaultValue(Enums.CostingMethods.None)]
        public Enums.CostingMethods CostingMethod { get; set; }

        [DefaultValue(Enums.CostPosingMethods.None)]
        public Enums.CostPosingMethods CostPosingMethod { get; set; }



        [DefaultValue(0)]
        public int? AmountOnQuotation { get; set; }
        public int? AmountOnSaleQuote { get; set; }
        public int? AmountOnSaleOrder { get; set; }

        [DefaultValue(0)]
        public int? AmountOnPurchaseOrder { get; set; }
        public int AmountOnPurchaseQuote { get; set; }

        [DefaultValue(0)]
        public int? AmountReorder { get; set; }



        public decimal LastestPurchaseCost { get; set; }
        [DefaultValue(0)]
        public int AmountSold { get; set; }
        [DefaultValue(0)]
        public int AmountPurchase { get; set; }

        [NotMapped]
        public int AmountOnHand => AmountPurchase - AmountSold + StockAmount;



        public int ReorderRequiredAmount
        {
            get
            {
                if (this.AmountOnHand < (this.AmountReorder ?? 0))
                    return (this.AmountReorder ?? 0) - this.AmountOnHand;
                else
                    return 0;
            }
        }


        [DefaultValue(0)]
        public int OpeningQty { get; set; }
        public Decimal OpeningCostPerUnit { get; set; }



        [DisplayName("COGS Account")]
        [Column("COGSAccountId")]
        public Guid? COGSAccountId { get; set; }
        [ForeignKey("COGSAccountId")]
        public virtual AccountItem COGSAccount { get; set; }


    }
}

