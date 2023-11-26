using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.DBContext;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class InventoryItems : ERPNodeDalRepository
    {

        public InventoryItems(Organization organization) : base(organization)
        {

        }

        public void UpdateStockAmount()
        {
            ////erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;

            organization.Items.RemoveUnLinkCommercialAndEstimateItems();
            this.ResetStockAmount();

            var Items = erpNodeDBContext
              .CommercialItems
              .Where(Ti => Ti.Item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
              .GroupBy(TransactionItem => new
              {
                  TransactionItem.ItemId,
                  TransactionItem.Commercial.TransactionType
              })
              .Select(go => new
              {
                  ItemId = go.Key.ItemId,
                  transactionType = go.Key.TransactionType,
                  Amount = go.Sum(i => i.Amount),
                  Item = go.FirstOrDefault().Item
              });

            foreach (var stockItem in Items)
            {
                if (stockItem.transactionType == ERPObjectType.Sale)
                    stockItem.Item.AmountSold = stockItem.Amount;

                else if (stockItem.transactionType == ERPObjectType.Purchase)
                    stockItem.Item.AmountPurchase = stockItem.Amount;
            }

            erpNodeDBContext.SaveChanges();
        }

        public void UpdateEstimateAmount()
        {
            organization.Items.RemoveUnLinkCommercialAndEstimateItems();

            var Items = erpNodeDBContext
              .EstimateItems
              .Where(Ti => Ti.Item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
              .GroupBy(TransactionItem => new
              {
                  TransactionItem.ItemId,
                  TransactionItem.Quote.TransactionType,
                  TransactionItem.Quote.Status,
              })
              .Select(go => new
              {
                  ItemId = go.Key.ItemId,
                  Status = go.Key.Status,
                  transactionType = go.Key.TransactionType,
                  Amount = go.Sum(i => i.Amount),
                  Item = go.FirstOrDefault().Item
              });


            foreach (var stockItem in Items)
            {
                var trType = stockItem.transactionType;
                var trStatus = stockItem.Status;
                if (trType == ERPObjectType.SaleQuote)
                {
                    if (trStatus == Models.Estimations.Enums.QuoteStatus.Quote)
                        stockItem.Item.AmountOnQuotation = (int)stockItem.Amount;
                    else if (trStatus == Models.Estimations.Enums.QuoteStatus.Ordered)
                        stockItem.Item.AmountOnSaleOrder = (int)stockItem.Amount;

                }
                else if(trType == ERPObjectType.PurchaseQuote)
                {
                    if (trStatus == Models.Estimations.Enums.QuoteStatus.Quote)
                        stockItem.Item.AmountOnPurchaseQuote = (int)stockItem.Amount;
                    else if (trStatus == Models.Estimations.Enums.QuoteStatus.Ordered)
                        stockItem.Item.AmountOnPurchaseOrder = (int)stockItem.Amount;
                }
                  
            }

            erpNodeDBContext.SaveChanges();
        }

        private void ResetStockAmount()
        {
            erpNodeDBContext
                 .Items
                 .Where(Ti => Ti.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                 .ToList()
                 .ForEach(i =>
                 {
                     i.AmountPurchase = 0;
                     i.AmountSold = 0;
                     i.AmountOnQuotation = 0;
                     i.AmountOnSaleOrder = 0;
                     i.AmountOnPurchaseOrder = 0;
                 });

            erpNodeDBContext.SaveChanges();
        }
    }
}
