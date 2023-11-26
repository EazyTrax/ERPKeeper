using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Financial.Payments
{
    [Table("ERP_Financial_General_Payments")]
    public class GeneralPayment
    {
        [Key]
        public Guid Id { get; set; }
        public LedgerPostStatus PostStatus { get; set; }
        public Enums.PaymentMethod PaymentMethod { get; set; }
        public Enums.PaymentStatus Status { get; set; }
        public ERPObjectType TransactionType { get; set; }
        public int No { get; set; }
        public int NoInMonth { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string PartialName { get; set; }
        public String Memo { get; set; }
        public String Reference { get; set; }
        public DateTime TrnDate { get; set; }
        public int Delay { get; set; }
        public DateTime ClearingDelay => TrnDate.AddDays(Delay);

        public virtual ICollection<Commercial> Commercials { get; set; }

        public string DocumentGroup => TrnDate.ToString("yyMM");
        public string DocumentCode => Helpers.ObjectsHelper.TrCode(TransactionType);

        public decimal LiabilityAmount { get; set; }
        public decimal CommercialAmount { get; set; }

        [Column("CommercialLinesTotalAmount")]
        public decimal CommercialBeforeTaxAmount { get; set; }
        public Decimal Amount => LiabilityAmount + CommercialAmount;
        public Decimal DiscountAmount { get; set; }
        public decimal AmountAfterDiscount => this.Amount - (this.DiscountAmount);
        public String AmountAfterDiscountThaiString => new KeeperCore.ERPNode.Helpers.Currency.Thai.Baht().Process(AmountAfterDiscount.ToString("N2"));
        public Decimal AmountRetention { get; set; }
        public Decimal AmountRetentionRate => 100 * AmountRetention / Math.Max(Amount, 1);
        public decimal AmountAfterDiscountAndRetention => this.Amount - (this.AmountRetention + this.DiscountAmount);
        public Decimal AmountPayFromOptionalAcc { get; set; }
        public decimal AmountPayFromPrimaryAcc => this.AmountAfterDiscountAndRetention - this.AmountPayFromOptionalAcc;


        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profiles.Profile Profile { get; set; }
        public string ProfileName { get; set; }
        public Decimal BankFeeAmount { get; set; }




        public Guid? OptionalAssetAccountId { get; set; }
        [ForeignKey("OptionalAssetAccountId")]
        public virtual AccountItem OptionalAssetAccount { get; set; }

        public Guid? AssetAccountId { get; set; }
        [ForeignKey("AssetAccountId")]
        public virtual AccountItem AssetAccount { get; set; }

        public Guid? LiabilityAccountId { get; set; }
        [ForeignKey("LiabilityAccountId")]
        public virtual AccountItem LiabilityAccount { get; set; }

        public Guid? ReceivableAccountId { get; set; }
        [ForeignKey("ReceivableAccountId")]
        public virtual AccountItem ReceivableAccount { get; set; }

        public Guid? RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual RetentionType RetentionType { get; set; }

        public GeneralPayment()
        {

        }


        public void UpdateName()
        {
            GroupName = DocumentGroup;
            PartialName = $"{DocumentGroup}/{NoInMonth}";
            Name = $"{DocumentCode}-{DocumentGroup}/{NoInMonth}";
        }

        public void CalculateAmount()
        {
            if (this.PostStatus == LedgerPostStatus.Locked)
                return;

            this.CalculateCommercialsAmount();
            this.AmountRetention = this.RetentionType?.GetRetentionAmount(this.CommercialBeforeTaxAmount) ?? 0;
            this.ProfileName = this.Profile?.Name ?? this.ProfileName;
        }


        public void AddCommercial(Commercial com)
        {
            if (this.Commercials == null)
                this.Commercials = new HashSet<Commercial>();

            this.Commercials.Add(com);
            //com.GeneralPaymentId = this.Id;
            //com.GeneralPayment = this;
            //com.UpdatePaymentStatus();

            this.CalculateCommercialsAmount();
        }

        public void RemoveCommercail(Commercial com)
        {
            this.Commercials.Remove(com);
            //////com.GeneralPaymentId = null;
            //////com.GeneralPayment = null;
            //////com.UpdatePaymentStatus();

            this.CalculateCommercialsAmount();
        }

        public void CalculateCommercialsAmount()
        {
            this.CommercialAmount = this.Commercials?.Sum(a => a.Total) ?? 0;
            this.CommercialBeforeTaxAmount = this.Commercials?.Sum(a => a.LinesTotal) ?? 0;
        }
    }
}