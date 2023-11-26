using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace ERPKeeper.Node.Models.Financial.Lends
{
    [Table("ERP_Finance_Lend_Payments")]
    public class LendPayment
    {
        [Key]
        public Guid Uid { get; set; }
        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.LendPayment;


        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }

        public int No { get; set; }


        public String Detail { get; set; }

        public Guid? LendGuid { get; set; }
        [ForeignKey("LendGuid")]
        public virtual Lend Lend { get; set; }

        public Guid? AssetAccountGuid { get; set; }
        [ForeignKey("AssetAccountGuid")]
        public virtual AccountItem AssetAccount { get; set; }



        public Guid? LiabilityAccountGuid { get; set; }
        [ForeignKey("LiabilityAccountGuid")]
        public virtual Accounting.AccountItem LiabilityAccount { get; set; }


        public LedgerPostStatus PostStatus { get; set; }




    }

}
