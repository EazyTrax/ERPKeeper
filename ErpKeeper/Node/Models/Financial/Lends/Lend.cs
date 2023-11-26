using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ERPKeeper.Node.Models.Transactions;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.Models.Financial.Lends
{
    [Table("ERP_Finance_Lends")]
    public class Lend
    {
        [Key]
        public Guid Uid { get; set; }
        public const Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.Lend;

        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }
        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public Accounting.FiscalYear FiscalYear { get; set; }
        public int No { get; set; }
        public string Name =>
            string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));
        public String Detail { get; set; }
        public Guid? AssetAccountGuid { get; set; }
        [ForeignKey("AssetAccountGuid")]
        public virtual AccountItem AssetAccount { get; set; }


        public Guid? LendingAssetAccountGuid { get; set; }
        [ForeignKey("LendingAssetAccountGuid")]
        public virtual Accounting.AccountItem LendingAssetAccount { get; set; }
        public virtual ICollection<LendPayment> Payments { get; set; }


        public LedgerPostStatus PostStatus { get; set; }


        [DefaultValue(0)]
        public decimal Amount { get; set; }
        public CommercialStatus Status { get; set; }
    }

}
