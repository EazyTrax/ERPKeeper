
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Items
{
    [Table("ERP_Item_ParameterTypes")]
    public class ItemParameterType
    {
        [Key]
        public Guid Uid { get; set; }
        public String Name { get; set; }
        public String Detail { get; set; }
    }
}