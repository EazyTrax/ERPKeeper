using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;


namespace ERPKeeper.Node.Models.Financial.Payments
{
    [Table("ERP_Finance_Payment_Retention_Group")]
    public class RetentionGroup
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TrnDate { get; set; }
        public virtual ICollection<GeneralPayment> GeneralPayments { get; set; }
        public Guid RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual RetentionType RetentionType { get; set; }
        public decimal AmountCommercial { get; set; }
        public decimal AmountRetention { get; set; }
        public int Count { get; set; }
        public int No { get; internal set; }

        public void Calculate()
        {
            Count = GeneralPayments.Count();
            AmountCommercial = GeneralPayments.Select(t => t.CommercialAmount).DefaultIfEmpty(0).Sum();
            AmountRetention = GeneralPayments.Select(t => t.AmountRetention).DefaultIfEmpty(0).Sum();
        }
    }

}