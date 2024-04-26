using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using KeeperCore.ERPNode.Models.Enums;

namespace KeeperCore.ERPNode.Models
{
    [Table("ERP_Datum")]
    public class DataItem
    {
        [Key]
        
        public Guid Id { get; set; }
        public DataItemKey Key { get; set; }
        public string Value { get; set; }
        public DataItemType DataType { get; set; }

    }
}