using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Items.Enums;
using System.IO;

namespace ERPKeeper.Node.DAL.Transactions
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
                .Where(ci => ci.TransactionGuid == null)
                .ToList();

            erpNodeDBContext.CommercialShipments
                .RemoveRange(removedUnreferenceCommercails);

            this.SaveChanges();
        }


        public CommercialShipment Find(Guid id) => erpNodeDBContext.CommercialShipments.Find(id);



        public MemoryStream ExportPDF(Guid shipmentId)
        {
            var transcation = erpNodeDBContext.CommercialShipments.Where(i => i.Uid == shipmentId).ToList();

            var report = new Node.Reports.Transactions.Commercial.ShippingLabel()
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