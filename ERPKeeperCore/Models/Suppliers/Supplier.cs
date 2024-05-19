using ERPKeeperCore.Enterprise.Models.Customers.Enums;
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
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public virtual Profiles.Profile Profile { get; set; }
        public Guid ProfileId => Profile.Id;
        public String? Code { get; set; }
        public SupplierStatus Status { get; set; }

        public Guid? DefaultTaxCodeUid { get; set; }
        [ForeignKey("DefaultTaxCodeUid")]
        public virtual Taxes.TaxCode DefaultTaxCode { get; set; }


        public Decimal TotalPurchases { get; set; }
        public int TotalPurchasesCount { get; set; }


        public Decimal TotalBalance { get; set; }
        public int TotalBalanceCount { get; set; }



        public Decimal CreditLimit { get; set; }
        public int CreditAgeLimit { get; set; }


        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public virtual ICollection<SupplierItem> SupplierItems { get; set; } = new List<SupplierItem>();


        public void SetActive()
        {
            this.Status = Enums.SupplierStatus.Active;
        }

        public void Update(Supplier supplier)
        {
            this.DefaultTaxCodeUid = supplier.DefaultTaxCodeUid;
            this.CreditAgeLimit = supplier.CreditAgeLimit;
            this.CreditLimit = supplier.CreditLimit;
        }

        public void UpdateBalance()
        {
            this.TotalPurchases = this.Purchases.Sum(x => x.LinesTotalAfterDiscount);
            this.TotalPurchasesCount = this.Purchases.Count();

            this.TotalBalance = this.Purchases.Where(x => x.Status == PurchaseStatus.Invoice).Sum(x => x.LinesTotalAfterDiscount);
            this.TotalBalanceCount = this.Purchases.Where(x => x.Status == PurchaseStatus.Invoice).Count();

        }

    }
}
