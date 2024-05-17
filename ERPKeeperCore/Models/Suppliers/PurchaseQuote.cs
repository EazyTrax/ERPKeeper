using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Suppliers
{
    public partial class PurchaseQuote
    {
        [Key]
        public Guid Id { get; set; }






        public Guid? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Suppliers.Supplier? Supplier { get; set; }

        public String? Reference { get; set; }
        public PurchaseQuoteStatus Status { get; set; }


        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

 
        public Decimal LinesTotal { get; set; }
        public Decimal Discount { get; set; }
        public Decimal LinesTotalAfterDiscount => LinesTotal - Discount;
        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotalAfterDiscount + Tax;

        public virtual ICollection<PurchaseQuoteItem> Items { get; set; } = new List<PurchaseQuoteItem>();

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }


        public Guid? SupplierAddressId { get; set; }

        public void UpdateBalance()
        {

            this.Name = $"PQ-{Date.Year}{Date.Month}-{this.No.ToString()}";

            this.LinesTotal = this.Items.Where(i => i.LineTotal > 0).Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotalAfterDiscount / 100;
            else
                this.Tax = 0;
        }
    }
}
