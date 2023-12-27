using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models.Transactions;


namespace KeeperCore.ERPNode.Models.Taxes
{
    [NotMapped]
    public class CommercialTaxGroup
    {
        public Guid? TaxCodeId { get; set; }
        public TaxCode TaxCode { get; set; }
        public decimal TaxBalance { get; set; }
        public int CommercialCount { get; set; }
        public List<Transaction> Commercials { get; set; }
    }
}
