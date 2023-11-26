using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace ERPKeeper.Node.Models.Financial.Loans
{
    [Table("ERP_Finance_Loan_Payments")]
    public class LoanPayment
    {
        [Key]
        public Guid Uid { get; set; }
        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.LoanPayment;
        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }

        public int No { get; set; }


        public String Detail { get; set; }

        public Guid? LoanGuid { get; set; }
        [ForeignKey("LoanGuid")]
        public virtual Loan Loan { get; set; }

        public Guid? AssetAccountGuid { get; set; }
        [ForeignKey("AssetAccountGuid")]
        public virtual AccountItem AssetAccount { get; set; }



        public Guid? LiabilityAccountGuid { get; set; }
        [ForeignKey("LiabilityAccountGuid")]
        public virtual Accounting.AccountItem LiabilityAccount { get; set; }


        public LedgerPostStatus PostStatus { get; set; }




    }

}
