using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Customers
{
    public partial class Customer
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public virtual Profiles.Profile Profile { get; set; }

        public Guid? ProfileId => Profile?.Id;

        public String? Code { get; set; }
        public CustomerStatus Status { get; set; }


        public Decimal TotalSales { get; set; }
        public int TotalSalesCount { get; set; }


        public Decimal TotalBalance { get; set; }
        public Decimal TotalBalanceCount { get; set; }



        public virtual ICollection<Sale> Sales { get; set; }


        public void Update(Customer customer)
        {

        }
        public void UpdateBalance()
        {
            this.TotalSales = this.Sales.Sum(x => x.LinesTotal);
            this.TotalSalesCount = this.Sales.Count;
        }

    }
}
