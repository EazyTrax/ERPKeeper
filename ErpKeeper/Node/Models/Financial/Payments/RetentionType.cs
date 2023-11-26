using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ERPKeeper.Node.Models.Financial.Payments
{
    [Table("ERP_Finance_RetentionTypes")]
    public class RetentionType
    {
        [Key]
        public Guid Uid { get; set; }
        public String Name { get; set; }
        public String PaymentType { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }
        public Enums.RetentionDirection RetentionDirection { get; set; }
        public Decimal Rate { get; set; }
        public Decimal GetRetentionAmount(decimal amount) => Math.Round(amount * Rate / 100, 2);

        public Guid? RetentionToAccountGuid { get; set; }
        [ForeignKey("RetentionToAccountGuid")]
        public virtual Accounting.AccountItem RetentionToAccount { get; set; }

        public virtual ICollection<GeneralPayment> GeneralPayments { get; set; }
        public virtual ICollection<RetentionGroup> RetentionGroups { get; set; }

        public void Update(RetentionType type)
        {
            this.Name = type.Name;
            this.PaymentType = type.PaymentType;
            this.Description = type.Description;
            this.RetentionToAccountGuid = type.RetentionToAccountGuid;
            this.IsActive = type.IsActive;
            this.Uid = type.Uid;
            this.Rate = type.Rate;
            this.RetentionDirection = type.RetentionDirection;
        }
    }
}
