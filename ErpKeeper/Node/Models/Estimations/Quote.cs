using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper;
using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeper.Node.Models.Estimations
{
    [Table("ERP_Quotes")]
    public class Quote
    {
        [Key]
        public Guid Uid { get; set; }
        public ERPObjectType TransactionType { get; set; }
 
        public int No { get; set; }

        public string Name { get; set; }
        public string GroupName { get; set; }
        public string PartialName { get; set; }

        public void UpdateName()
        {
            GroupName = DocumentGroup;
            PartialName = string.Format("{0}/{1}", DocumentGroup, No.ToString().PadLeft(3, '0'));
            Name = string.Format("{0}-{1}/{2}", DocumentCode, DocumentGroup, No.ToString().PadLeft(3, '0'));
        }

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
                    return ERPKeeper.Helpers.Badge.Danger(this.Age);
                else if (this.Age > 60)
                    return ERPKeeper.Helpers.Badge.Warning(this.Age);
                else if (this.Age > 30)
                    return ERPKeeper.Helpers.Badge.Info(this.Age);
                else
                    return ERPKeeper.Helpers.Badge.Dark(this.Age); ;
            }
        }

        public Enums.QuoteStatus Status { get; set; }
        public String Reference { get; set; }
        public String Memo { get; set; }

      


        public Guid? ProjectGuid { get; set; }
        [ForeignKey("ProjectGuid")]
        public virtual Models.Projects.Project Project { get; set; }

        public Guid? ProfileGuid { get; set; }
        [ForeignKey("ProfileGuid")]
        public virtual Profiles.Profile Profile { get; set; }
        public string ProfileName { get; set; }

        public Guid? ProfileAddressGuid { get; set; }
        [ForeignKey("ProfileAddressGuid")]
        public virtual Profiles.ProfileAddress ProfileAddress { get; set; }


        public Guid? ResponsibleGuid { get; set; }


        /// <summary>
        /// Profile of Self Organization
        /// </summary>
        public Guid? SelfProfileGuid { get; set; }
        [ForeignKey("SelfProfileGuid")]
        public virtual Profiles.Profile SelfProfile { get; set; }


        public Guid? TaxCodeGuid { get; set; }
        [ForeignKey("TaxCodeGuid")]
        public virtual Models.Taxes.TaxCode TaxCode { get; set; }

        public Guid? PaymentTermGuid { get; set; }
        [ForeignKey("PaymentTermGuid")]
        public virtual PaymentTerm PaymentTerm { get; set; }


        public Guid? ShippingTermGuid { get; set; }
        [ForeignKey("ShippingTermGuid")]
        public virtual ShippingTerm ShippingTerm { get; set; }

        public Guid? ShippingMethodGuid { get; set; }
        [ForeignKey("ShippingMethodGuid")]
        public virtual ShippingMethod ShippingMethod { get; set; }

        public virtual ICollection<QuoteItem> Items { get; set; }


        [NotMapped]
        public List<QuoteItem> ExportItems => this.Items?.ToList();
  
        public void RemoveItem(QuoteItem existEstItem) => this.Items.Remove(existEstItem);

        public decimal LinesTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public String TotalThaiCurrencyString => new ERPKeeper.Helpers.Currency.Thai.Baht().Process(Total);


        [DefaultValue(false)]
        public bool IsPaymentComplete { get; set; }

        public string PaymentMemo { get; set; }
        public DateTime? PaymentDate { get; set; }


        [Column("PaymentAssetAccountUid")]
        public Guid? AssetAccountUid { get; set; }
        [ForeignKey("AssetAccountUid")]
        public virtual ERPKeeper.Node.Models.Accounting.AccountItem AssetAccount { get; set; }



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
                Uid = Guid.NewGuid(),
                ItemGuid = item.Uid,
                Amount = amount,
                ItemPartNumber = item.PartNumber,
                ItemDescription = item.Description,
                UnitPrice = item.UnitPrice,
                Order = order,
            };

            Items.Add(estimateItem);
            this.Calculate();
        }
    }
}
