
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Items
{
    [Table("ERP_Item_Parameters")]
    public class ItemParameter
    {
        [Key]
        public Guid Uid { get; set; }

        public Guid? ItemUid { get; set; }
        [ForeignKey("ItemUid")]
        public virtual Item Item { get; set; }

        public Guid? ParameterTypeUid { get; set; }
        [ForeignKey("ParameterTypeUid")]
        public virtual ItemParameterType ParameterType { get; set; }
        public string Value { get; set; }
    }
}