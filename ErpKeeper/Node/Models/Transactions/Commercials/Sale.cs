using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPKeeper.Node.Models.Accounting;

namespace ERPKeeper.Node.Models.Transactions.Commercials
{
    public class Sale : Commercial
    {

        public Sale()
        {
            Uid = Guid.NewGuid();
            this.TrnDate = DateTime.Now;
            this.Status = CommercialStatus.Open;
            this.TrnDate = DateTime.Now;
            this.TransactionType = Accounting.Enums.ERPObjectType.Sale;
        }
        public void UpdateAddress()
        {
            if (this.ProfileAddress == null && this.Profile.PrimaryAddress != null)
            {
                this.ProfileAddress = this.Profile.PrimaryAddress;
                this.ProfileAddressGuid = this.Profile.PrimaryAddress.AddressGuid;
            }
        }

        public void UpdateSerialNumber()
        {
            CommercialItems.ToList().ForEach(item => item.UpdateExportSerialNumber());
        }

        
    }
}
