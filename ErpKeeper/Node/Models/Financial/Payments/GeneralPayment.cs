using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace ERPKeeper.Node.Models.Financial.Payments
{
    [Table("ERP_Financial_General_Payments")]
    public class GeneralPayment
    {
        [Key]
        public Guid Uid { get; set; }
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

        //WILL BE DELETE SOON
        public Guid? CommercialUid { get; set; }
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
        public String AmountAfterDiscountThaiString => new ERPKeeper.Helpers.Currency.Thai.Baht().Process(AmountAfterDiscount.ToString("N2"));
        public Decimal AmountRetention { get; set; }
        public Decimal AmountRetentionRate => 100 * AmountRetention / Math.Max(Amount, 1);
        public decimal AmountAfterDiscountAndRetention => this.Amount - (this.AmountRetention + this.DiscountAmount);
        public Decimal AmountPayFromOptionalAcc { get; set; }
        public decimal AmountPayFromPrimaryAcc => this.AmountAfterDiscountAndRetention - this.AmountPayFromOptionalAcc;


        public Guid? ProfileGuid { get; set; }
        [ForeignKey("ProfileGuid")]
        public virtual Profiles.Profile Profile { get; set; }
        public string ProfileName { get; set; }

        public Guid? CompanyProfileGuid { get; set; }
        [ForeignKey("CompanyProfileGuid")]
        public virtual Profiles.Profile CompanyProfile { get; set; }

        public Decimal BankFeeAmount { get; set; }

        #region Account
        public Guid? OptionalAssetAccountUid { get; set; }
        [ForeignKey("OptionalAssetAccountUid")]
        public virtual AccountItem OptionalAssetAccount { get; set; }

        public Guid? AssetAccountUid { get; set; }
        [ForeignKey("AssetAccountUid")]
        public virtual AccountItem AssetAccount { get; set; }

        public Guid? LiabilityAccountUid { get; set; }
        [ForeignKey("LiabilityAccountUid")]
        public virtual AccountItem LiabilityAccount { get; set; }

        public Guid? ReceivableAccountUid { get; set; }
        [ForeignKey("ReceivableAccountUid")]
        public virtual AccountItem ReceivableAccount { get; set; }

        #endregion

        public Guid? RetentionTypeGuid { get; set; }
        [ForeignKey("RetentionTypeGuid")]
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

        #region Commercial
        public void AddCommercial(Commercial com)
        {
            if (this.Commercials == null)
                this.Commercials = new HashSet<Commercial>();

            this.Commercials.Add(com);
            com.GeneralPaymentUid = this.Uid;
            com.GeneralPayment = this;
            com.UpdatePaymentStatus();

            this.CalculateCommercialsAmount();
        }

        public void RemoveCommercail(Commercial com)
        {
            this.Commercials.Remove(com);
            com.GeneralPaymentUid = null;
            com.GeneralPayment = null;
            com.UpdatePaymentStatus();

            this.CalculateCommercialsAmount();
        }

        public void CalculateCommercialsAmount()
        {
            this.CommercialAmount = this.Commercials?.Sum(a => a.Total) ?? 0;
            this.CommercialBeforeTaxAmount = this.Commercials?.Sum(a => a.LinesTotal) ?? 0;
        }


        #endregion

        #region retentions
        public DateTime RetentionGroupDate => new DateTime(TrnDate.Year, TrnDate.Month, 1);

        [Column("RetentionGroupId")]
        public Guid? RetentionGroupId { get; set; }
        [ForeignKey("RetentionGroupId")]
        public RetentionGroup RetentionGroup { get; set; }


        public string GetThaiRDReport(int order)
        {
            string date = this.TrnDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
            string profile = this.Profile.Name;
            string taxId = this.Profile.TaxNumber;

            string address = this.Profile.PrimaryAddress.AddressLine;
            if (address != null && address.Length > 30)
            {
                address = address.Substring(0, 28);
                address = address.Replace(Environment.NewLine, "");
            }

            string branchNo = this.Profile.PrimaryAddress.Number;


            string retentionStr = "";
            retentionStr += $"{this.RetentionType.Name}";
            retentionStr += $"|{order}";
            retentionStr += $"|{taxId}";
            retentionStr += $"|{branchNo}";
            retentionStr += $"|{this.Profile.Name}";//5
            retentionStr += $"|{address}";
            retentionStr += $"|{date}";
            retentionStr += $"|{this.RetentionType.PaymentType}";
            retentionStr += $"|{this.RetentionType.Rate}";
            retentionStr += $"|{this.CommercialBeforeTaxAmount}";
            retentionStr += $"|{this.AmountRetention}";//15
            retentionStr += $"|1";
            return retentionStr;
        }
        #endregion
    }
}




