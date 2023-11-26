using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeeperCore.ERPNode.Models.ChartOfAccount
{
    [Table("ERP_Accounting_Default_Account")]
    public class DefaultAccountItem
    {
        [Key]
        public Enums.SystemAccountType AccountType { get; set; }

        public Guid? AccountItemId { get; set; }
        [ForeignKey("AccountItemId")]
        public virtual AccountItem AccountItem { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}