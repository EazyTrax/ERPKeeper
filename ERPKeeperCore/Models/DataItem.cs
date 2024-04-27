using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ERPKeeperCore.Enterprise.Models.Enums;

namespace ERPKeeperCore.Enterprise.Models
{
    public class DataItem
    {
        [Key]
        
        public Guid Id { get; set; }
        public DataItemKey Key { get; set; }
        public String? Value { get; set; }
        public DataItemType DataType { get; set; }

    }
}