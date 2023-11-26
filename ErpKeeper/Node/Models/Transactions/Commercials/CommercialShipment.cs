using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ERPKeeper.Node.Models.Profiles;

namespace ERPKeeper.Node.Models.Transactions
{

    [Table("ERP_Transactions_Commercial_Shipments")]
    public class CommercialShipment
    {
        [Key]
        public Guid Uid { get; set; }

        public DateTime Shipdate { get; set; }
        public String TrackingNo { get; set; }
        public String Method { get; set; }
        public String Note { get; set; }
        public Guid? ProfileAddressId { get; set; }
        [ForeignKey("ProfileAddressId")]
        public virtual ProfileAddress Address { get; set; }

        public Guid? ProfileContactId { get; set; }
        [ForeignKey("ProfileContactId")]
        public virtual ProfileContact Contact { get; set; }


        [Column("TransactionGuid")]
        public Guid? TransactionGuid { get; set; }
        [ForeignKey("TransactionGuid")]
        public virtual Commercial Commercial { get; set; }

        public CommercialShipment()
        {
            Uid = Guid.NewGuid();
        }
    }
}
