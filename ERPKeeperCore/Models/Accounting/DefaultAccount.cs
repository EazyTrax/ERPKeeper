using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Accounting
{
    [Table("ERP_Accounting_Default_Account")]
    public class DefaultAccount
    {
        [Key]
        public SystemAccountType AccountType { get; set; }

        public Guid? AccountItemId { get; set; }
        [ForeignKey("AccountItemId")]
        public virtual Account AccountItem { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}