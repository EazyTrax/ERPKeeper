using DevExpress.CodeParser;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Items.Enums;
using KeeperCore.ERPNode.Models.Taxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace KeeperCore.ERPNode.Models.Transactions
{
    public partial class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public TransactionStatus Status { get; set; }
        public String Memo { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public DateTime TrnDate { get; set; } = DateTime.Now;
        public Guid? FiscalYearId { get; set; }
        
        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profiles.Profile Profile { get; set; }

        public Guid? ProfileAddressId { get; set; }
        [ForeignKey("ProfileAddressId")]
        public virtual Profiles.ProfileAddress ProfileAddress { get; set; }

        public Guid? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Projects.Project Project { get; set; }

   

        public Decimal LinesTotal { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Total { get; set; }
 
        public Guid? PaymentTermId { get; set; }
        [ForeignKey("PaymentTermId")]
        public virtual PaymentTerm PaymentTerm { get; set; }
      
        public virtual ICollection<TransactionItem> CommercialItems { get; set; }
      
        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode TaxCode { get; set; }

        public Guid? TaxPeriodId { get; set; }
        [ForeignKey("TaxPeriodId")]
        public virtual Models.Taxes.TaxPeriod TaxPeriod { get; set; }
    }
}
