using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public partial class TransactionLedger
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account? Account { get; set; }

        public String? Memo { get; set; }    

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
