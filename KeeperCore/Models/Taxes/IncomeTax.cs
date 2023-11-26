using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Taxes
{
    [Table("ERP_Taxes_IncomeTaxes")]
    public class IncomeTax
    {
        [Key]
        public Guid Id { get; set; }
        public int No { get; set; }

        public DateTime TrDate { get; set; }
        public string Name => string.Format("INT{0}-{1}", this.TrDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));

        public Decimal ProfitAmount { get; set; }
        public Decimal TaxAmount { get; set; }
        public String Memo { get; set; }

        public bool Update(IncomeTax incomeTax)
        {
            if (this.PostStatus != LedgerPostStatus.Locked)
            {
                this.TrDate = incomeTax.TrDate;
                this.ProfitAmount = incomeTax.ProfitAmount;
                this.TaxAmount = incomeTax.TaxAmount;
                this.IncomeTaxExpenseAccountId = incomeTax.IncomeTaxExpenseAccountId;
                this.LiabilityAccountId = incomeTax.LiabilityAccountId;
                return true;
            }

            return false;
        }

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual Accounting.FiscalYear FiscalYear { get; set; }

        public Guid? LiabilityAccountId { get; set; }
        [ForeignKey("LiabilityAccountId")]
        public virtual AccountItem LiabilityAccount { get; set; }


        public Guid? IncomeTaxExpenseAccountId { get; set; }
        [ForeignKey("IncomeTaxExpenseAccountId")]
        public virtual AccountItem IncomeTaxExpenAccount { get; set; }

        public Guid? PayFromAccountId { get; set; }
        public LedgerPostStatus PostStatus { get; set; }

    }
}
