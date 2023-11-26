using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.Models.Financial.Loans
{
    [Table("ERP_Finance_Loans")]
    public class Loan
    {
        [Key]
        public Guid Uid { get; set; }
        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.Loan;
        public Models.Transactions.CommercialStatus Status { get; set; }

        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }
        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public Accounting.FiscalYear FiscalYear { get; set; }

        public int No { get; set; }

        public string Name =>
            string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));

        public String Detail { get; set; }

        public decimal Amount { get; set; }

        public Guid? AssetAccountGuid { get; set; }
        [ForeignKey("AssetAccountGuid")]
        public virtual AccountItem AssetAccount { get; set; }



        public Guid? LiabilityAccountGuid { get; set; }
        [ForeignKey("LiabilityAccountGuid")]
        public virtual Accounting.AccountItem LiabilityAccount { get; set; }


        public virtual ICollection<LoanPayment> Payments { get; set; }


        public LedgerPostStatus PostStatus { get; set; }

    }

}
