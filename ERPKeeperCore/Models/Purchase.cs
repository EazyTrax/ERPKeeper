
using KeeperCore.ERPNode.Models.Enums;
using KeeperCore.ERPNode.Models.Transactions;
using KeeperCore.ERPNode.Models.Transactions.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace KeeperCore.ERPNode.Models
{
    public partial class Purchase
    {
        [Key]
        public Guid Id { get; set; }
        public CommericalTransactionStatus Status { get; set; }
        public string? Memo { get; set; }
        public int No { get; set; }
        public string? Name { get; set; }
        public DateTime TrnDate { get; set; } = DateTime.Now;
        public Guid? FiscalYearId { get; set; }

        public decimal LinesTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }

        public Guid? TaxPeriodId { get; set; }
        [ForeignKey("TaxPeriodId")]
        public virtual TaxPeriod? TaxPeriod { get; set; }
    }
}
