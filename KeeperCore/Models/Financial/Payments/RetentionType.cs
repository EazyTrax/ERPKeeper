using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace KeeperCore.ERPNode.Models.Financial.Payments
{
    [Table("ERP_Finance_RetentionTypes")]
    public class RetentionType
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String PaymentType { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }
        public Enums.RetentionDirection RetentionDirection { get; set; }
        public Decimal Rate { get; set; }
        public Decimal GetRetentionAmount(decimal amount) => Math.Round(amount * Rate / 100, 2);

        public Guid? RetentionToAccountId { get; set; }
        [ForeignKey("RetentionToAccountId")]
        public virtual ChartOfAccount.AccountItem RetentionToAccount { get; set; }

        public virtual ICollection<GeneralPayment> GeneralPayments { get; set; }

        public void Update(RetentionType type)
        {
            this.Name = type.Name;
            this.PaymentType = type.PaymentType;
            this.Description = type.Description;
            this.RetentionToAccountId = type.RetentionToAccountId;
            this.IsActive = type.IsActive;
            this.Id = type.Id;
            this.Rate = type.Rate;
            this.RetentionDirection = type.RetentionDirection;
        }
    }
}
