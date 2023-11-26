using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Transactions.Commercials
{

    [Table("ERP_ShippingMethods")]
    public class ShippingMethod
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }

        public String Name { get; set; }
        public String Detail { get; set; }

        public void Update(ShippingMethod term)
        {
            this.Name = term.Name ?? this.Name;
            this.Detail = term.Detail;
        }
    }



}
