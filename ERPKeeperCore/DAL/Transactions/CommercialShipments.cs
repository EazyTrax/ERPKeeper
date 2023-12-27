using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Items.Enums;
using System.IO;

namespace KeeperCore.ERPNode.DAL.Transactions
{
    public class CommercialShipments : ERPNodeDalRepository
    {
        public CommercialShipments(Organization organization) : base(organization)
        {
        }

        public List<CommercialShipment> All() => erpNodeDBContext.CommercialShipments.ToList();

        public IQueryable<CommercialShipment> Query() => erpNodeDBContext.CommercialShipments;



        internal void RemoveUnAssign()
        {
            var removedUnreferenceCommercails = erpNodeDBContext.CommercialShipments
                .Where(ci => ci.TransactionId == null)
                .ToList();

            erpNodeDBContext.CommercialShipments
                .RemoveRange(removedUnreferenceCommercails);

            this.SaveChanges();
        }


        public CommercialShipment Find(Guid id) => erpNodeDBContext.CommercialShipments.Find(id);



        public MemoryStream ExportPDF(Guid shipmentId)
        {
            var transcation = erpNodeDBContext.CommercialShipments.Where(i => i.Id == shipmentId).ToList();

            var report = new ERPNode.Reports.Transactions.Commercial.ShippingLabel()
            {
                DataSource = transcation,
                Name = "EXP"
            };
            MemoryStream ms = new MemoryStream();
            report.ExportToPdf(ms);

            return ms;
        }
    }
}