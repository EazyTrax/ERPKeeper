using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Transactions.Commercials
{
    [Table("ERP_ShippingTerms")]
    public class ShippingTerm
    {
        [Key]
        [Column("GID")]
        public Guid Uid { get; set; }
        public String Name { get; set; }
        public String Detail { get; set; }

        public void Update(ShippingTerm term)
        {
            this.Name = term.Name;
            this.Detail = term.Detail;
        }
    }
}
