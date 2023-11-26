using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Taxes
{
    [Table("ERP_Taxes_TaxGroups")]
    public class TaxGroup
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public bool isDefault { get; set; }
        public String Code { get; private set; }


    
    }
}
