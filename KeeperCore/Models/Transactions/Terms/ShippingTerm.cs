using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Transactions.Commercials
{
    [Table("ERP_ShippingTerms")]
    public class ShippingTerm
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Detail { get; set; }

        public void Update(ShippingTerm term)
        {
            this.Name = term.Name;
            this.Detail = term.Detail;
        }
    }
}
