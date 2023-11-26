using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    public class SaleHistory
    {
        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal TotalAmount { get; set; }
    }
}