﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeper.Node.Models.Suppliers
{
    [Table("ERP_Suppliers")]
    public class Supplier
    {
        [Key, ForeignKey("Profile")]
        public Guid ProfileUid { get; set; }
        public virtual Profiles.Profile Profile { get; set; }
        public string Code { get; set; }
        public Enums.SupplierStatus Status { get; set; }


        public Guid? DefaultTaxCodeUid { get; set; }
        [ForeignKey("DefaultTaxCodeUid")]
        public virtual Taxes.TaxCode DefaultTaxCode { get; set; }



        public int CountPurchases { get; set; }
        public Decimal SumPurchaseBalance { get; set; }
        public Decimal TotalBalance { get; internal set; }


        public int CountBalance { get; internal set; }

        public Decimal CreditLimit { get; set; }
        public int CreditAgeLimit { get; set; }


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
    }
}
