using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperCore.ERPNode.Models.Customers
{
    [Table("ERP_Customers")]
    public class Customer
    {
        [Key, ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        public virtual Profiles.Profile Profile { get; set; }

        public string Code { get; set; }
        public Enums.CustomerStatus Status { get; set; }

        public int CountSales { get; set; }
        public Decimal TotalSale { get; set; }
        public Decimal TotalBalance { get; set; }
        public decimal CountBalance { get; set; }

        public void Update(Customer customer)
        {
           
        }
    }
}
