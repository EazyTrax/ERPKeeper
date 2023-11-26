using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Transactions.Commercials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace KeeperCore.ERPNode.Models.Estimations
{
    [Table("ERP_Quotes")]
    public class Quote
    {
        [Key]
        public Guid Id { get; set; }
        public ERPObjectType TransactionType { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string PartialName { get; set; }

   
        public string DocumentGroup => TrnDate.ToString("yyMM");
        public string DocumentCode => Helpers.ObjectsHelper.TrCode(TransactionType);

        public DateTime TrnDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? CloseDate { get; set; }

        public int ExpiredInDayCount { get; set; }



        public int Age => (int)((CloseDate ?? DateTime.Now) - TrnDate).TotalDays;
        public string AgeBadge
        {
            get
            {
                if (this.Age > 120)
                    return KeeperCore.ERPNode.Helpers.Badge.Danger(this.Age);
                else if (this.Age > 60)
                    return KeeperCore.ERPNode.Helpers.Badge.Warning(this.Age);
                else if (this.Age > 30)
                    return KeeperCore.ERPNode.Helpers.Badge.Info(this.Age);
                else
                    return KeeperCore.ERPNode.Helpers.Badge.Dark(this.Age); ;
            }
        }

        public Enums.QuoteStatus Status { get; set; }
        public String Reference { get; set; }
        public String Memo { get; set; }


        public Guid? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Models.Tasks.Project Project { get; set; }

        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profiles.Profile Profile { get; set; }
        public string ProfileName { get; set; }

        public Guid? ProfileAddressId { get; set; }
        [ForeignKey("ProfileAddressId")]
        public virtual Profiles.ProfileAddress ProfileAddress { get; set; }



        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Security.User Member { get; set; }




        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual Models.Taxes.TaxCode TaxCode { get; set; }

        public Guid? PaymentTermId { get; set; }
        [ForeignKey("PaymentTermId")]
        public virtual PaymentTerm PaymentTerm { get; set; }


        public Guid? ShippingTermId { get; set; }
        [ForeignKey("ShippingTermId")]
        public virtual ShippingTerm ShippingTerm { get; set; }

        public Guid? ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual ICollection<QuoteItem> Items { get; set; }
        public void RemoveItem(QuoteItem existEstItem) => this.Items.Remove(existEstItem);



        public decimal LinesTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public String TotalThaiCurrencyString => new KeeperCore.ERPNode.Helpers.Currency.Thai.Baht().Process(Total);


        [DefaultValue(false)]
        public bool IsPaymentComplete { get; set; }

        public string PaymentMemo { get; set; }
        public DateTime? PaymentDate { get; set; }


        [Column("PaymentAssetAccountId")]
        public Guid? AssetAccountId { get; set; }
        [ForeignKey("AssetAccountId")]
        public virtual KeeperCore.ERPNode.Models.ChartOfAccount.AccountItem AssetAccount { get; set; }

        public virtual ICollection<QuoteItem> QuoteItems { get; set; }


        public void UpdateName()
        {
            GroupName = DocumentGroup;
            PartialName = string.Format("{0}/{1}", DocumentGroup, No.ToString().PadLeft(3, '0'));
            Name = string.Format("{0}-{1}/{2}", DocumentCode, DocumentGroup, No.ToString().PadLeft(3, '0'));
        }


        public void Calculate()
        {

            this.ProfileName = this.Profile?.Name ?? this.ProfileName;
            this.LinesTotal = Items?.Sum(estimateItem => estimateItem.LineTotal) ?? 0;
            this.Tax = this.TaxCode?.GetTaxBalance(this.TrnDate, this.LinesTotal) ?? 0;
            this.Total = this.LinesTotal + this.Tax;
        }


        public void ReOrder()
        {
            int i = 0;

            Items.OrderBy(item => item.Order).ToList().ForEach(item =>
              {
                  item.Order = ++i;
              });
        }

        public void AddItem(Items.Item item, int amount)
        {
            if (Items == null)
                Items = new HashSet<QuoteItem>();
            int order = Items.Count + 1;
            var estimateItem = new QuoteItem()
            {
                Id = Guid.NewGuid(),
                ItemId = item.Id,
                Amount = amount,
                ItemPartNumber = item.PartNumber,
                ItemDescription = item.Description,
                UnitPrice = item.SalePrice,
                Order = order,
            };

            Items.Add(estimateItem);
            this.Calculate();
        }
    }
}
