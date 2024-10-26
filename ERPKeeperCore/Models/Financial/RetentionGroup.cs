using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Financial.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERPKeeperCore.Enterprise.Models.Financial
{
    public class RetentionGroup
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate => new DateTime(Date.Year, Date.Month, 1);
        public DateTime EndDate => new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month));


        public Guid RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual RetentionType RetentionType { get; set; }

        public virtual ICollection<ReceivePayment> ReceivePayments { get; set; } = new List<ReceivePayment>();
        public virtual ICollection<SupplierPayment> SupplierPayments { get; set; } = new List<SupplierPayment>();


        public decimal AmountCommercial { get; set; }
        public decimal AmountRetention { get; set; }
        public int Count { get; set; }
        public int No { get; internal set; }

        public void Calculate()
        {
            Count = ReceivePayments.Count() + SupplierPayments.Count();
            AmountCommercial = ReceivePayments.Select(t => t.AmountTotal).DefaultIfEmpty(0).Sum() +
                SupplierPayments.Select(t => t.AmountAfterDiscount).DefaultIfEmpty(0).Sum();


            AmountRetention = ReceivePayments.Select(t => t.AmountRetention).DefaultIfEmpty(0).Sum() +
                 SupplierPayments.Select(t => t.AmountRetention).DefaultIfEmpty(0).Sum();
        }
    }


}