using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using KeeperCore.ERPNode.Models.Transactions;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Financial.Lends
{
    [Table("ERP_Finance_Lends")]
    public class Lend
    {
        [Key]
        public Guid Id { get; set; }
        public const Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.Lend;

        
        public DateTime TrnDate { get; set; }
        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual Accounting.FiscalYear FiscalYear { get; set; }
        public int No { get; set; }
        public string Name =>
            string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));
        public String Detail { get; set; }



    

        public Guid? CreditFrom_AssetAccountId { get; set; }
        [ForeignKey("CreditFrom_AssetAccountId")]
        public virtual AccountItem CreditFrom_AssetAccount { get; set; }


        public Guid? DebitTo_AssetAccountId { get; set; }
        [ForeignKey("DebitTo_AssetAccountId")]
        public virtual ChartOfAccount.AccountItem DebitTo_AssetAccount { get; set; }


        public virtual ICollection<LendPayment> Payments { get; set; }


        public LedgerPostStatus PostStatus { get; set; }


        [DefaultValue(0)]
        public decimal Amount { get; set; }
        public CommercialStatus Status { get; set; }
    }

}
