using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Customers
{
    public class SaleQuote
    {
        [Key]
        public Guid Id { get; set; }
        public SaleQuoteStatus Status { get; set; }
        public bool IsPosted { get; set; }



        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customers.Customer? Customer { get; set; }


        public Guid? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Projects.Project? Project { get; set; }

        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Decimal LinesTotal { get; set; }
        public Decimal Discount { get; set; }
        public Decimal LinesTotalAfterDiscount => LinesTotal - Discount;
        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotalAfterDiscount + Tax;







        public virtual ICollection<SaleQuoteItem> Items { get; set; } = new List<SaleQuoteItem>();

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }

        public Guid? CustomerAddressId { get; set; }

        public void AddItem(SaleQuoteItem existItem)
        {
            this.Items.Add(existItem);
        }

        public void UpdateBalance()
        {

            this.Name = $"SQ-{Date.Year}{Date.Month}-{this.No.ToString()}";

            this.LinesTotal = this.Items.Where(i => i.LineTotal > 0).Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotalAfterDiscount / 100;
            else
                this.Tax = 0;
        }
    }
}
