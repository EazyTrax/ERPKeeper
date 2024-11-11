using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models.Financial
{
    public class PaymentTerm
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int DueDayCount { get; set; }


        public decimal DiscountPercent { get; set; }


        public int MaxDayCount { get; set; }

        public void Update(PaymentTerm term)
        {
            Name = term.Name;
            DiscountPercent = term.DiscountPercent;
            DueDayCount = term.DueDayCount;
        }
    }
}
