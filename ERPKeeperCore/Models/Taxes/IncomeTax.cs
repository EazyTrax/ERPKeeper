
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Taxes
{
    public class IncomeTax
    {
        [Key]
        public Guid Uid { get; set; }
        public int No { get; set; }
        public bool IsPosted { get; set; }
        public DateTime TrDate { get; set; }
        public string Name => string.Format("INT{0}-{1}", this.TrDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));

        public Decimal ProfitAmount { get; set; }
        public Decimal TaxAmount { get; set; }
        public String Memo { get; set; }

        public bool Update(IncomeTax incomeTax)
        {
            return false;
        }

        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public virtual Accounting.FiscalYear FiscalYear { get; set; }

        public Guid? LiabilityAccountGuid { get; set; }
        [ForeignKey("LiabilityAccountGuid")]
        public virtual Accounting.Account LiabilityAccount { get; set; }


        public Guid? IncomeTaxExpenseAccountGuid { get; set; }
        [ForeignKey("IncomeTaxExpenseAccountGuid")]
        public virtual Accounting.Account IncomeTaxExpenAccount { get; set; }

        public Guid? PayFromAccountGuid { get; set; }
        

    }
}
