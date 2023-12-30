using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models
{
    [Table("ERP_Datum")]
    public class DataItem
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public DataItemKey Key { get; set; }



        public string Value { get; set; }
        public DataType DataType { get; set; }


        public string ValueString { get; set; }
        public string ValueDecimal { get; set; }
        public string VlaueInt { get; set; }
        public string VlaueDateTime { get; set; }

    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Type can be moved to separate file")]
    public enum DataType
    {
        String = 0,
        Decimal = 1,
        Int = 2,
        DateTime = 3

    }
}