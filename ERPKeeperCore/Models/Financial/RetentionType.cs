﻿using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Financial.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Financial
{
    public class RetentionType
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String? PaymentType { get; set; }
        public String? Description { get; set; }
        public bool IsActive { get; set; }
        public Enums.RetentionDirection Direction { get; set; }
        public Decimal Rate { get; set; }
        public Decimal GetRetentionAmount(decimal amount) => Math.Round(amount * Rate / 100, 2);

        public Guid? RetentionToAccountId { get; set; }
        [ForeignKey("RetentionToAccountId")]
        public virtual Accounting.Account? RetentionToAccount { get; set; }

        public virtual ICollection<ReceivePayment> ReceivePayments { get; set; }=new List<ReceivePayment>();
        public virtual ICollection<SupplierPayment> SupplierPayments { get; set; }= new List<SupplierPayment>();

        public void Update(RetentionType type)
        {
            this.Name = type.Name;
            this.PaymentType = type.PaymentType;
            this.Description = type.Description;
            this.RetentionToAccountId = type.RetentionToAccountId;
            this.IsActive = type.IsActive;
            this.Rate = type.Rate;
            this.Direction = type.Direction;
        }
    }


}