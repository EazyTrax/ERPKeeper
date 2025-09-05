using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Financial;
using ERPKeeperCore.Enterprise.Models.Items;
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

        public Guid? ProfileAddesssId { get; set; }
        [ForeignKey("ProfileAddesssId")]
        public virtual Profiles.ProfileAddress? ProfileAddesss { get; set; }

        public Guid? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Projects.Project? Project { get; set; }

        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public Decimal LinesTotal { get; set; }
        public Decimal Discount { get; set; }
        public Decimal LinesTotalAfterDiscount => LinesTotal - Discount;

        public Decimal Tax { get; set; }

        public Decimal Total => LinesTotalAfterDiscount + (IsPriceTaxInclude ? 0 : Tax);

        public bool IsPriceTaxInclude { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }


        public DateTime? CloseDate { get; set; }
        public int AgeInDays
        {
            get
            {
                var endDate = CloseDate ?? DateTime.Today;
                return (endDate - Date).Days;
            }
        }
        public string AgeColor
        {
            get
            {
                if (AgeInDays < 30)
                    return "blue";
                else if (AgeInDays < 60)
                    return "yellow";
                else
                    return "red";
            }
        }


        public virtual ICollection<SaleQuoteItem> Items { get; set; } = new List<SaleQuoteItem>();

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }

        public Guid? SaleId { get; internal set; }


        public Guid? PaymentTermId { get; set; }
        [ForeignKey("PaymentTermId")]
        public virtual PaymentTerm? PaymentTerm { get; set; }
        public void AddItem(SaleQuoteItem existItem)
        {
            this.Items.Add(existItem);
        }

        public void AddItem(Item item)
        {
            var saleQuoteItem = new SaleQuoteItem()
            {
                ItemId = item.Id,
                PartNumber = item.PartNumber,
                Description = item.Description,
                Price = item.SalePrice,
                Quantity = 1,
                Order = this.Items.Count() + 1,
            };
            this.Items.Add(saleQuoteItem);
        }

        public void CopyItems(ICollection<SaleQuoteItem> originalItems)
        {
            foreach (var originalItem in originalItems)
            {
                var newItem = new SaleQuoteItem()
                {
                    ItemId = originalItem.ItemId,
                    PartNumber = originalItem.PartNumber,
                    Description = originalItem.Description,
                    Price = originalItem.Price,
                    Quantity = originalItem.Quantity,
                    Order = this.Items.Count() + 1,
                };
                this.Items.Add(newItem);
            }
        }

        public void Reorder()
        {
            int index = 1;
            this.Items = this.Items.OrderBy(i => i.Order).ToList();

            foreach (var item in Items)
            {
                item.Order = index++;
            }
        }

        public void SetStatus(SaleQuoteStatus newStatus)
        {
            if (Status == SaleQuoteStatus.Close)
                return;

            this.Status = newStatus;
        }

        public void UpdateAddress()
        {
            if (this.ProfileAddesssId != null)
                return;



            this.ProfileAddesssId = this?.Customer?
                .Profile?
                .Addresses?
                .FirstOrDefault()?.Id;

        }

        public void UpdateBalance()
        {

            this.LinesTotal = this.Items.Where(i => i.LineTotal > 0).Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotalAfterDiscount / 100;
            else
                this.Tax = 0;
        }

        public void UpdateItems()
        {
            var removeItems = this.Items
                 .Where(i => i.Quantity == 0)
                 .ToList();

            foreach (var item in removeItems)
            {
                this.Items.Remove(item);
            }

            this.Reorder();
            this.UpdateBalance();
        }

        public void UpdateName()
        {
            var currentYear = this.Date.Year;
            var currentMonth = this.Date.Month;

            this.Name = $"SQ-{currentYear}/{currentMonth}/{this.No.ToString()}";
        }


    }
}
