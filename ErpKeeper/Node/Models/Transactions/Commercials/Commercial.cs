using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Items.Enums;
using ERPKeeper.Node.Models.Taxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace ERPKeeper.Node.Models.Transactions
{
    [Table("ERP_Transactions_Commercial")]
    public partial class Commercial
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }
        public ERPObjectType TransactionType { get; set; }
        public String QRId => Uid.ToString("D");
        public String QRDisplay => Uid.ToString("D").Replace("-", Environment.NewLine);
        public LedgerPostStatus PostStatus { get; set; }
        public String DocumentTypeName => "ใบกำกับภาษี/ใบวางบิล";
        public CommercialStatus Status { get; set; }

        //public int Version { get; set; }
        public String Log { get; set; }
        public String Memo { get; set; }
        public bool IsOnline { get; set; }
        public int No { get; set; }
        public int NoInMonth { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string PartialName { get; set; }

        public DateTime TrnDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public String ThaiDate => TrnDate.ToString("dd-MMM-yy", new CultureInfo("th-TH"));
        public String ThaiCloseDate => (CloseDate ?? DateTime.Now).ToString("dd-MMM-yy", new CultureInfo("th-TH"));
        public int Age => (int)((CloseDate ?? DateTime.Now) - TrnDate).TotalDays;
        public string AgeBadge => ERPKeeper.Helpers.Badge.GetBadge(Age);


        public void UpdateName()
        {
            GroupName = DocumentGroup;
            PartialName = $"{DocumentGroup}/{NoInMonth.ToString().PadLeft(3, '0')}";
            Name = $"{DocumentCode}-{PartialName}";
        }

        public string DocumentGroup => TrnDate.ToString("yyMM");
        public string DocumentCode => Helpers.ObjectsHelper.TrCode(TransactionType);
        public Guid? FiscalYearUid { get; set; }



        public Guid? CompanyProfileGuid { get; set; }
        [ForeignKey("CompanyProfileGuid")]
        public virtual Profiles.Profile CompanyProfile { get; set; }

        public Guid? CompanyProfileAddressGuid { get; set; }
        [ForeignKey("CompanyProfileAddressGuid")]
        public virtual Profiles.ProfileAddress CompanyProfileAddress { get; set; }



        public Guid? ProfileGuid { get; set; }
        [ForeignKey("ProfileGuid")]
        public virtual Profiles.Profile Profile { get; set; }
        public string ProfileName { get; set; }
        public Guid? ProfileAddressGuid { get; set; }
        [ForeignKey("ProfileAddressGuid")]
        public virtual Profiles.ProfileAddress ProfileAddress { get; set; }
        public string CacheProfileName { get; set; }
        public string CacheProfileAddress { get; set; }





        public Guid? ProjectGuid { get; set; }
        [ForeignKey("ProjectGuid")]
        public virtual Projects.Project Project { get; set; }

        [MaxLength(200)]
        public String Reference { get; set; }
        public bool IsAudit { get; set; }
        public bool IsFocus { get; set; }
        public String FocusDetail { get; set; }
        public String AuditDetail { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public double CloseAge => ((CloseDate ?? DateTime.Now) - TrnDate).TotalDays;
        public Decimal LinesTotal { get; set; }
        public Decimal Tax { get; set; }
        public String IsTax => (Tax > 0) ? "TAX" : "";
        public Decimal TaxOffset { get; set; }
        public Decimal Total { get; set; }
        public String TotalTHB { get; set; }
        public String ThaiCurrencyString => new ERPKeeper.Helpers.Currency.Thai.Baht().Process(Total);


        #region Payment
        public Decimal TotalPayment => (GeneralPaymentUid == null) ? 0 : Total;
        public Decimal TotalBalance => Total - TotalPayment;
        public Guid? GeneralPaymentUid { get; set; }
        [ForeignKey("GeneralPaymentUid")]
        public virtual Financial.Payments.GeneralPayment GeneralPayment { get; set; }

        [Column("PaymentAssetAccount")]
        public Guid? PaymentAccountId { get; set; }

        //Payment Section
        public void UpdatePaymentStatus()
        {
            if (this.Status == CommercialStatus.Void)
                return;
            this.Status = (this.GeneralPaymentUid == null) ? CommercialStatus.Open : CommercialStatus.Paid;
            this.CloseDate = this.GeneralPayment?.TrnDate;
        }
        #endregion


        public Guid? PaymentTermGuid { get; set; }
        [ForeignKey("PaymentTermGuid")]
        public virtual Commercials.PaymentTerm PaymentTerm { get; set; }
        public Guid? ShippingTermGuid { get; set; }
        [ForeignKey("ShippingTermGuid")]
        public virtual Commercials.ShippingTerm ShippingTerm { get; set; }
        public Guid? ShippingMethodGuid { get; set; }
        [ForeignKey("ShippingMethodGuid")]
        public virtual Commercials.ShippingMethod ShippingMethod { get; set; }


        public virtual ICollection<CommercialItem> CommercialItems { get; set; }
        public virtual ICollection<CommercialShipment> CommercialShipments { get; set; }


        public void Update(Commercial commercial)
        {
            if (PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Transaction Posted");

            NoInMonth = commercial.NoInMonth;
            Reference = commercial.Reference;
            Memo = commercial.Memo;
            ProjectGuid = commercial.ProjectGuid;
            TrnDate = commercial.TrnDate;
            PaymentTermGuid = commercial.PaymentTermGuid;
            ShippingTermGuid = commercial.ShippingTermGuid;
            No = commercial.No;
            TaxCodeId = commercial.TaxCodeId;


            ProfileName = Profile?.Name ?? ProfileName;
            commercial.UpdateName();
        }
        public void ReCalculate()
        {
            Console.WriteLine("> Calculate");
            this.ProfileName = Profile?.Name ?? this.ProfileName;

            this.SortItemsOrder();

            this.LinesTotal = Math.Round(this.CommercialItems?.ToList().Sum(i => i.LineTotal) ?? 0, 2, MidpointRounding.ToEven);
            this.Tax = this.TaxCode?.GetTaxBalance(this.TrnDate, this.LinesTotal) ?? 0;
            this.Total = this.LinesTotal + this.Tax;

            this.UpdatePaymentStatus();
        }



        #region Tax
        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode TaxCode { get; set; }

        public Guid? TaxPeriodId { get; set; }
        [ForeignKey("TaxPeriodId")]
        public virtual Models.Taxes.TaxPeriod TaxPeriod { get; set; }
        public int TaxPeriodOrder { get; set; }



        #endregion


        //Items Sections
        [NotMapped]
        public virtual List<CommercialItem> ExportCommercialItems => CommercialItems?.ToList().ToList();
        public void SortItemsOrder()
        {
            if (CommercialItems == null)
                return;

            var commecialItems = CommercialItems.OrderBy(item => item.Order)
                .ToList();

            int i = 1;
            foreach (var item in commecialItems)
            {
                item.Order = i;
                i++;
            }
        }
        public CommercialItem AddItem(Items.Item item, int amount)
        {
            Log = string.Format("{0}{1}{2}", Log, "> Add item " + item.PartNumber.ToString(), Environment.NewLine);

            if (CommercialItems == null)
                CommercialItems = new HashSet<CommercialItem>();

            if (!ValidateItemType(item)) return null;

            int order = CommercialItems.Count + 1;
            var commecialItem = new CommercialItem()
            {
                Uid = Guid.NewGuid(),
                ItemGuid = item.Uid,
                Item = item,
                ItemPartNumber = item.PartNumber,
                ItemDescription = item.Description,
                UnitPrice = item.UnitPrice,
                Amount = amount,
                TransactionType = TransactionType,
                Order = order
            };

            CommercialItems.Add(commecialItem);

            return commecialItem;
        }
        private bool ValidateItemType(Items.Item item)
        {
            switch (TransactionType)
            {
                case Accounting.Enums.ERPObjectType.Sale:
                case Accounting.Enums.ERPObjectType.SalesReturn:
                    if (item.ItemType != ItemTypes.Expense)
                        return true;
                    else
                        return false;

                case Accounting.Enums.ERPObjectType.Purchase:
                case Accounting.Enums.ERPObjectType.PurchaseReturn:
                    if (item.ItemType != ItemTypes.Service)
                        return true;
                    else
                        return false;
                default:
                    return false;
            }
        }
        public void RemoveItems()
        {
            foreach (var item in CommercialItems.ToList())
            {
                CommercialItems.Remove(item);
            }
        }
        public void RemoveItem(Guid itemUid)
        {
            var item = CommercialItems
                .Where(Item => Item.ItemGuid == itemUid)
                .FirstOrDefault();

            if (item != null)
            {
                CommercialItems.Remove(item);
            }

        }

        public CommercialShipment CreatShipment(string method, string tracking)
        {
            if (CommercialShipments == null)
                CommercialShipments = new HashSet<CommercialShipment>();

            var commercialShipment = new CommercialShipment()
            {
                Uid = Guid.NewGuid(),
                Method = method,
                TrackingNo = tracking,
                Shipdate = DateTime.Now,
            };

            CommercialShipments.Add(commercialShipment);
            return commercialShipment;
        }

    }
}
