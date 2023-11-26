using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Items
{
    [Table("ERP_Item_SerialNumbers")]
    public partial class ItemSerialNumber
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public string No { get; set; }

        public string PartNumber { get; set; }
        public Guid? ItemId { get; set; }


    }
}