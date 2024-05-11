using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel;
using ERPKeeperCore.Enterprise.Models.Assets.Enums;

namespace ERPKeeperCore.Enterprise.Models.Assets
{
    public class ObsoleteAsset
    {
        [Key]
        public Guid Id { get; set; }


        public Guid? AssetId { get; set; }
        [ForeignKey("AssetId")]
        public virtual Asset? Asset { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }

        public DateTime ObsoleteDate { get; set; }

        internal void PostToTransaction()
        {
            Console.WriteLine($">Post >ObsoleteAsset:{this.Asset.Name}");

            if (this.Transaction == null) return;
            this.Transaction.ClearLedger();
            this.Transaction.Date = this.ObsoleteDate;
            this.Transaction.PostedDate = DateTime.Now;

            // Dr.
            this.Transaction.AddDebit(this.Asset.AssetType.AccumulateDeprecateAcc, this.Asset.TotalDepreciationValue);

            // Cr.
            this.Transaction.AddCredit(this.Asset.AssetType.AwaitDeprecateAccount, this.Asset.TotalDepreciationValue);

            this.IsPosted = true;
            this.Transaction.UpdateBalance();
        }

    }
}
