using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models.Enums;

namespace KeeperCore.ERPNode.Models
{
    [Table("ERP_Accounting_Default_Account")]
    public class DefaultAccount
    {
        [Key]
        public DefaultAccountType Type { get; set; }

        public Guid? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}