using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace KeeperCore.ERPNode.Models.Financial.Loans
{
    [Table("ERP_Finance_Loan_Payments")]
    public class LoanPayment
    {
        [Key]
        public Guid Id { get; set; }
        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.LoanPayment;
        
        public DateTime TrnDate { get; set; }

        public int No { get; set; }


        public String Detail { get; set; }

        public Guid? LoanId { get; set; }
        public virtual Loan Loan { get; set; }

        public Guid? AssetAccountId { get; set; }
        public virtual AccountItem AssetAccount { get; set; }

        public Guid? LoanAccountId { get; set; }
        public virtual ChartOfAccount.AccountItem LoanAccount { get; set; }

        public LedgerPostStatus PostStatus { get; set; }
    }

}
