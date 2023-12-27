using KeeperCore.ERPNode.Models.Accounting.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Accounting
{

    [Table("ERP_Equity_CapitalActivities")]
    public class CapitalActivity
    {
        [Key]

        public Guid Id { get; set; }
        public DateTime TrnDate { get; set; }

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual FiscalYear FiscalYear { get; set; }

        public Enums.CapitalActivityType Type { get; set; }

        public Enums.ERPObjectType TransactionType = Enums.ERPObjectType.CapitalActivity;
        public int? No { get; set; }

        [NotMapped]
        public string Name => string.Format("{0}/{1}", TrnDate.ToString("yyMM"), No.ToString().PadLeft(3, '0'));


        public Guid? InvestorId { get; set; }
        [ForeignKey("InvestorId")]
        public virtual Models.Equity.Investor Investor { get; set; }

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


        [Timestamp]
        public byte[] RowVersion { get; set; }

        public void CaluculateTotal()
        {
            if (Type == Enums.CapitalActivityType.Invest)
                StockAmount = Math.Abs(StockAmount);
            else
                StockAmount = -1 * Math.Abs(StockAmount);
        }

        public CapitalActivity()
        {

        }

    }
}