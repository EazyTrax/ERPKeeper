using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Financial.Loans
{
    [Table("ERP_Finance_Loans")]
    public class Loan
    {
        [Key]
        public Guid Id { get; set; }
        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.Loan;
        public Models.Transactions.CommercialStatus Status { get; set; }

        
        public DateTime TrnDate { get; set; }
        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual Accounting.FiscalYear FiscalYear { get; set; }

        public int No { get; set; }
        public string Name => string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));

        public String Detail { get; set; }
        public decimal Amount { get; set; }

        public Guid? DebitTo_AssetAccountId { get; set; }
        [ForeignKey("DebitTo_AssetAccountId")]
        public virtual AccountItem DebitTo_AssetAccount { get; set; }

        public Guid? CreditTo_LiabilityAccountId { get; set; }
        [ForeignKey("CreditTo_LiabilityAccountId")]
        public virtual ChartOfAccount.AccountItem CreditTo_LiabilityAccount { get; set; }

        public virtual ICollection<LoanPayment> LoanPayments { get; set; }

        public LedgerPostStatus PostStatus { get; set; }

    }

}
