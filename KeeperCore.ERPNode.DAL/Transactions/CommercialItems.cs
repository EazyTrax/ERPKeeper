﻿using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Items.Enums;

namespace KeeperCore.ERPNode.DAL.Transactions
{
    public class CommercialItems : ERPNodeDalRepository
    {
        public CommercialItems(Organization organization) : base(organization)
        {
        }

        public List<CommercialItem> All() => erpNodeDBContext.CommercialItems.ToList();

        public IQueryable<CommercialItem> Query() => erpNodeDBContext.CommercialItems;

        internal void RemoveUnAssign()
        {
            var removedUnreferenceCommercails = erpNodeDBContext.CommercialItems
                .Where(ci => ci.TransactionId == null)
                .ToList();

            erpNodeDBContext.CommercialItems
                .RemoveRange(removedUnreferenceCommercails);

            this.SaveChanges();
        }

        public CommercialItem Find(Guid id) => erpNodeDBContext.CommercialItems.Find(id);

        public List<CommercialItem> Get(ItemTypes inventory)
        {
            return erpNodeDBContext
                    .CommercialItems
                    .Where(c => c.Item.ItemType == KeeperCore.ERPNode.Models.Items.Enums.ItemTypes.Inventory)
                    .ToList();
        }

        public List<CommercialItem> Get(ItemTypes inventory, Guid fiscalId)
        {
            var fiscal = erpNodeDBContext.FiscalYears.Find(fiscalId);
            var startDate = fiscal.StartDate;
            var endDate = fiscal.EndDate;

            return erpNodeDBContext.CommercialItems
           .Where(c => c.Item.ItemType == ItemTypes.Inventory)
           .Where(c => c.Commercial.TrnDate >= startDate && c.Commercial.TrnDate <= endDate)
           .ToList();
        }
    }
}