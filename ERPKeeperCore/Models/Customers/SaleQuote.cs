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

    

        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customers.Customer? Customer { get; set; }




        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Decimal LinesTotal { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotal + Tax;

        public virtual ICollection<SaleQuoteItem> Items { get; set; } = new List<SaleQuoteItem>();

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }

        public Guid? CustomerAddressId { get; set; }

        public void AddItem(SaleQuoteItem existItem)
        {
            this.Items.Add(existItem);
        }

    
    }
}
