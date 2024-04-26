using KeeperCore.ERPNode.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models
{
    public class CapitalActivity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TrnDate { get; set; }

        public int No { get; set; }

        [NotMapped]
        public string Name => string.Format("{0}/{1}", TrnDate.ToString("yyMM"), No.ToString().PadLeft(3, '0'));
        public int StockAmount { get; set; }

        [DisplayFormat(DataFormatString = "N2")]
        [Column(TypeName = "Money")]
        public decimal EachStockParValue { get; set; }


        [DisplayFormat(DataFormatString = "N2")]
        [Column(TypeName = "Money")]
        public decimal TotalStockParValue { get { return EachStockParValue * StockAmount; } }

        [DisplayFormat(DataFormatString = "N2")]
        [Column(TypeName = "Money")]
        public decimal TotalStockValue { get { return TotalStockParValue; } }



        public Guid? EquityAccountId { get; set; }
        [ForeignKey("EquityAccountId")]
        public virtual Account EquityAccount { get; set; }

        public Guid? AssetAccountId { get; set; }
        [ForeignKey("AssetAccountId")]
        public virtual Account AssetAccount { get; set; }

        public LedgerPostStatus PostStatus { get; set; }


        public CapitalActivity()
        {

        }

    }
}