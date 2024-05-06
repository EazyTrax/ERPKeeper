using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeperCore.Enterprise.Models.Financial
{
    public class LiabilityPaymentPayFromAccount
    {
        [Key]
        public Guid Id { get; set; }

        public Guid LiabilityPaymentId { get; set; }
        [ForeignKey("LiabilityPaymentId")]
        public virtual LiabilityPayment LiabilityPayment { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Accounting.Account Account { get; set; }

        public Decimal Amount { get; set; }
    }
}