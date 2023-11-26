using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Items
{
    [Table("ERP_Items_DataSheets")]
    public class DataSheet
    {
        [Key]
        public Guid Uid { get; set; }
        public Guid ItemGuid { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public string Name { get; set; }
    }
}