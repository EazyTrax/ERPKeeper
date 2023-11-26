using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeper.Node.Models.Accounting
{
    [Table("ERP_Accounting_Default_Account")]
    public class DefaultAccountItem
    {
        [Key]
        public Enums.SystemAccountType AccountType { get; set; }

        public Guid? AccountItemUid { get; set; }
        [ForeignKey("AccountItemUid")]
        public virtual AccountItem AccountItem { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}