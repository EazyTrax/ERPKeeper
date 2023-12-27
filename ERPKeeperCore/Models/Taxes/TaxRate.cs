using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Taxes
{


    [Table("ERP_Taxes_TaxRate")]
    public class TaxRate
    {
        [Key]
        [Column("Id")]
        public Guid TaxRateId { get; set; }

        [DefaultValue(0)]
        public Decimal Rate { get; set; }


        
        public DateTime AsOf { get; set; }

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode TaxCode { get; set; }

        public TaxRate()
        {
           
        }


    }
}