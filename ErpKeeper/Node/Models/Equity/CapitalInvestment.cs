using ERPKeeper.Node.Models.Accounting.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Equity
{

    [Table("ERP_Equity_CapitalActivities")]
    public class CapitalActivity
    {
        [Key]

        public Guid Uid { get; set; }


        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }

        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public Accounting.FiscalYear FiscalYear { get; set; }

        public Enums.CapitalActivityType Type { get; set; }

        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.CapitalActivity;

        [Column("No")]
        public int? No { get; set; }

        [NotMapped]
        public string Name => string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));


        public Guid? InvestorUid { get; set; }
        [ForeignKey("InvestorUid")]
        public virtual ERPKeeper.Node.Models.Equity.Investor Investor { get; set; }

        public int StockAmount { get; set; }

        [DisplayFormat(DataFormatString = "N2")]
        [Column(TypeName = "Money")]
        public Decimal EachStockParValue { get; set; }


        [DisplayFormat(DataFormatString = "N2")]
        [Column(TypeName = "Money")]
        public Decimal TotalStockParValue { get { return EachStockParValue * StockAmount; } }



        [DisplayFormat(DataFormatString = "N2")]
        [Column(TypeName = "Money")]
        public Decimal TotalStockValue { get { return TotalStockParValue; } }


        [Column("EquityAccountGuid")]
        public Guid? EquityAccountGuid { get; set; }
        [ForeignKey("EquityAccountGuid")]
        public virtual ERPKeeper.Node.Models.Accounting.AccountItem EquityAccount { get; set; }



        [Column("AssetAccountGuid")]
        public Guid? AssetAccountGuid { get; set; }
        [ForeignKey("AssetAccountGuid")]
        public virtual ERPKeeper.Node.Models.Accounting.AccountItem AssetAccount { get; set; }

        public LedgerPostStatus PostStatus { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }

        public void CaluculateTotal()
        {
            if (this.Type == Enums.CapitalActivityType.Invest)
                StockAmount = Math.Abs(StockAmount);
            else
                StockAmount = -1 * Math.Abs(StockAmount);
        }

        public CapitalActivity()
        {
            Uid = Guid.NewGuid();
        }

    }
}