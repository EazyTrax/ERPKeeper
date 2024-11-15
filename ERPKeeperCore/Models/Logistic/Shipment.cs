using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Logistic
{
    public partial class Shipment
    {
        [Key]
        public Guid Id { get; set; }
        public string? LotNo { get; set; }
        public String? Person { get; set; }
        public String? Note { get; set; }
        public String? TrackingNo { get; set; }
        public DateTime Date { get; set; }


        public Guid TransactionId { get; set; }

        public Guid ShipmentProviderId { get; set; }
        [ForeignKey("ShipmentProviderId")]
        public virtual LogisticProvider ShipmentProvider { get; set; }
  
    }


    public partial class LogisticProvider
    {
        [Key]
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String WebSite { get; set; }



        public virtual ICollection<Shipment> Shipments { get; set; }
    }

}
