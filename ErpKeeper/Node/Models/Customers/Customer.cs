using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeper.Node.Models.Customers
{
    [Table("ERP_Customers")]
    public class Customer
    {
        [Key, ForeignKey("Profile")]
        public Guid ProfileUid { get; set; }
        public virtual Profiles.Profile Profile { get; set; }
        public string Code { get; set; }
        public Enums.CustomerStatus Status { get; set; }

        public int CountSales { get; set; }
        [Column("TotalSale")]
        public Decimal SumSaleBalance { get; set; }

        [Column("TotalBalance")]
        public Decimal TotalBalance { get; set; }
        public decimal CountBalance { get; set; }

        public void Update(Customer customer)
        {
           
        }
    }
}
