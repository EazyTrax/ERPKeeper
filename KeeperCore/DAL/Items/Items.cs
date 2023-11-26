using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.Items.Enums;
using KeeperCore.ERPNode.Models.Transactions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace KeeperCore.ERPNode.DAL.Items
{
    public class Items : ERPNodeDalRepository
    {
        public Items(Organization organization) : base(organization)
        {

        }

        public IQueryable<Item> Query() => erpNodeDBContext.Items;



        public List<Item> ListAll() => this.Query().ToList();
        public List<Item> GetAll() => this.Query().ToList();

        public IQueryable<Item> GetInventories => this.Query().Where(items => items.ItemType == ItemTypes.Inventory);
        public IQueryable<Item> GetNonInventories => this.Query().Where(items => items.ItemType == ItemTypes.NonInventory);
        public List<Item> GetItems(ERPObjectType transactionType)
        {
            switch (transactionType)
            {
                case ERPObjectType.Purchase:
                    return erpNodeDBContext.Items.ToList();
                case ERPObjectType.Sale:
                    return erpNodeDBContext.Items
                        .Where(i => i.ItemType != ItemTypes.Expense)
                        .ToList();
            }

            return null;
        }


        public int Count(ItemTypes type) =>
         erpNodeDBContext
         .Items
         .Where(t => t.ItemType == type)
         .Count();

        public List<Item> ListByType(ItemTypes type) =>
            erpNodeDBContext
            .Items
            .Where(t => t.ItemType == type)
            .ToList();

        public List<Item> ListByBrand(Guid itemBrandId) =>
            erpNodeDBContext
            .Items
            .Where(t => t.BrandId == itemBrandId)
            .ToList();

        public void Delete(Guid id)
        {
            var item = erpNodeDBContext.Items.Find(id);

            if (item.CommercialItems.Count == 0)
                erpNodeDBContext.Items.Remove(item);

            erpNodeDBContext.SaveChanges();
        }
        public void Merge(Guid sourceItemId, Guid targetItemId)
        {
            erpNodeDBContext.CommercialItems.Where(ci => ci.ItemId == sourceItemId).ToList().ForEach(i =>
            {
                i.Item.Status = KeeperCore.ERPNode.Models.ChartOfAccount.ItemStatus.InActive;
                i.ItemId = targetItemId;
            });

            erpNodeDBContext.EstimateItems.Where(ci => ci.ItemId == sourceItemId).ToList().ForEach(i =>
            {
                i.Item.Status = KeeperCore.ERPNode.Models.ChartOfAccount.ItemStatus.InActive;
                i.ItemId = targetItemId;
            });

            erpNodeDBContext.PeriodItemsCOGS.Where(ci => ci.ItemId == sourceItemId).ToList().ForEach(i =>
            {
                i.Item.Status = KeeperCore.ERPNode.Models.ChartOfAccount.ItemStatus.InActive;
                i.ItemId = targetItemId;
            });
            organization.SaveChanges();

            erpNodeDBContext.Items.Remove(erpNodeDBContext.Items.Find(sourceItemId));
            organization.SaveChanges();
        }

        public Item Copy(Item originalItem)
        {
            var cloneItem = this.erpNodeDBContext.Items
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == originalItem.Id);

            cloneItem.Id = Guid.NewGuid();
            cloneItem.PartNumber = "Clone-" + cloneItem.PartNumber;
            cloneItem.Description = "Clone-" + cloneItem.Description;

            this.erpNodeDBContext.Items.Add(cloneItem);
            this.erpNodeDBContext.SaveChanges();

            return cloneItem;
        }

        public Item Save(Item item)
        {
            var existItem = erpNodeDBContext.Items.Find(item.Id);

            if (existItem == null)
            {
                item.Id = Guid.NewGuid();
                erpNodeDBContext.Items.Add(item);

                if (item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                    item.PurchaseAccount = item.PurchaseAccount ?? erpNodeDBContext.AccountItems
                        .Where(i => i.SubType == Models.ChartOfAccount.AccountSubTypes.Inventory)
                        .FirstOrDefault();

                item.COGSAccount = item.COGSAccount ?? erpNodeDBContext.AccountItems
                    .Where(i => i.SubType == Models.ChartOfAccount.AccountSubTypes.CostOfGoodsSold)
                    .FirstOrDefault();

                item.IncomeAccount = item.IncomeAccount ?? erpNodeDBContext.AccountItems
                    .Where(i => i.SubType == Models.ChartOfAccount.AccountSubTypes.Income)
                    .FirstOrDefault();

                erpNodeDBContext.SaveChanges();
            }
            else
            {
                existItem.PartNumber = item.PartNumber.Trim();
                existItem.UPC = (item.UPC ?? " ").Trim();
                existItem.Description = item.Description.Trim();
                existItem.SalePrice = item.SalePrice;
                existItem.BrandId = item.BrandId;
                existItem.Status = item.Status;
                existItem.ParentId = item.ParentId;
                existItem.PriceRangeId = item.PriceRangeId;
                existItem.Specification = item.Specification;
                existItem.OnlineSale = item.OnlineSale;
                existItem.IncomeAccountId = item.IncomeAccountId;
                existItem.PurchaseAccountId = item.PurchaseAccountId;
                existItem.QuickFinderKey = item.QuickFinderKey;
                existItem.QuickFinderValue = item.QuickFinderValue;

                if (item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                {

                }
                else if (item.ItemType == Models.Items.Enums.ItemTypes.Asset)
                {
                    existItem.FixedAssetTypeId = item.FixedAssetTypeId;

                }
                erpNodeDBContext.SaveChanges();
            }
            return item;
        }

        public Item Search(string searchDocument)
        {
            var item = erpNodeDBContext.Items
                .Where(c => c.PartNumber == searchDocument || c.UPC == searchDocument)
                .FirstOrDefault();
            return item;
        }

        public void Create(Item tecItem)
        {
            var item = new Item()
            {
                Id = tecItem.Id,
                PartNumber = tecItem.PartNumber,
                SalePrice = tecItem.SalePrice,
                PurchasePrice = tecItem.PurchasePrice,
                ItemType = tecItem.ItemType,
                Description = tecItem.Description,
                OnlineSale = tecItem.OnlineSale,
                IncomeAccountId = organization.SystemAccounts.Income.Id,
                PurchaseAccount = erpNodeDBContext.AccountItems
                        .Where(i => i.SubType == Models.ChartOfAccount.AccountSubTypes.Inventory)
                        .FirstOrDefault(),

                COGSAccount = erpNodeDBContext.AccountItems
                .Where(i => i.SubType == Models.ChartOfAccount.AccountSubTypes.CostOfGoodsSold)
                .FirstOrDefault()

            };
            erpNodeDBContext.Items.Add(item);
            erpNodeDBContext.SaveChanges();
        }
        public Item FindByPartNumber(string partNumber)
        {
            if (partNumber == null)
                return null;

            return erpNodeDBContext.Items
                .Where(i => i.PartNumber.ToUpper() == partNumber)
                .FirstOrDefault();
        }

        public Item Find(Guid? id, bool disableLazyLoading = false)
        {
            if (!disableLazyLoading)
                return erpNodeDBContext.Items.Find(id);

            //erpNodeDBContext.Configuration.LazyLoadingEnabled = false;
            var Item = erpNodeDBContext.Items.Find(id);
            // erpNodeDBContext.Configuration.LazyLoadingEnabled = true;
            return Item;

        }
        public Item SelectRandom() => this.Query().OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault();
        private Item Create(ItemTypes type, string partNumber, string description, decimal salePrice)
        {
            var newItem = new KeeperCore.ERPNode.Models.Items.Item()
            {
                Id = Guid.NewGuid(),
                ItemType = type,
                PartNumber = partNumber,
                Description = description,
                SalePrice = salePrice
            };

            newItem.IncomeAccountId = organization.SystemAccounts.Income.Id;
            newItem.PurchaseAccountId = organization.SystemAccounts.Expense.Id;

            erpNodeDBContext.Items.Add(newItem);
            erpNodeDBContext.SaveChanges();

            return newItem;
        }
        public void UpdateUnUsed()
        {
            var removeCommercialItems = erpNodeDBContext.CommercialItems
                          .Where(c => c.Commercial == null);

            erpNodeDBContext.CommercialItems.RemoveRange(removeCommercialItems);

            var removeQuoteItems = erpNodeDBContext.EstimateItems
                 .Where(c => c.QuoteId == null);

            erpNodeDBContext.EstimateItems.RemoveRange(removeQuoteItems);
            erpNodeDBContext.SaveChanges();

            erpNodeDBContext.Items.ToList()
                .ForEach(i =>
                {
                    var commercialCount = i.CommercialItems.Count;
                    var estimateCount = i.QuoteItems.Count;


                    if (commercialCount == 0)
                    {
                        Console.WriteLine(i.PartNumber + "=>" + commercialCount + "," + estimateCount);
                        erpNodeDBContext.Items.Remove(i);
                        erpNodeDBContext.SaveChanges();
                    }


                });

        }


        public void RemoveUnLinkCommercialAndEstimateItems()
        {
            string sqlCommand = "DELETE FROM [dbo].[ERP_Transactions_Commercial_Items] WHERE TransactionGuid IS NULL";
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
            erpNodeDBContext.SaveChanges();


            sqlCommand = "DELETE FROM [dbo].[ERP_Quotes_Items] WHERE QuoteId IS NULL";
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
            erpNodeDBContext.SaveChanges();


        }


        public int AmountService => this.Query().Where(i => i.ItemType == ItemTypes.Service).Count();
        public int AmountNonInventory => this.Query().Where(i => i.ItemType == ItemTypes.NonInventory).Count();
        public int AmountInventory => this.Query().Where(i => i.ItemType == ItemTypes.Inventory).Count();
        public int AmountExpense => this.Query().Where(i => i.ItemType == ItemTypes.Expense).Count();
        public int AmountAsset => this.Query().Where(i => i.ItemType == ItemTypes.Asset).Count();
        public List<Item> ListOnlineSale() => this.Query().Where(i => i.OnlineSale).ToList();
        public List<Item> ListItems(Guid groupId) =>
            this.Query()
            .Where(i => i.ItemType != ItemTypes.Group && i.ParentId == groupId)
            .ToList();
    }
}
