using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Items;
using ERPKeeper.Node.Models.Items.Enums;
using ERPKeeper.Node.Models.Transactions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeper.Node.DAL.Items
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
            .Where(t => t.BrandGuid == itemBrandId)
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
            erpNodeDBContext.CommercialItems.Where(ci => ci.ItemGuid == sourceItemId).ToList().ForEach(i =>
            {
                i.Item.Status = ERPKeeper.Node.Models.Accounting.ItemStatus.InActive;
                i.ItemGuid = targetItemId;
            });

            erpNodeDBContext.EstimateItems.Where(ci => ci.ItemGuid == sourceItemId).ToList().ForEach(i =>
            {
                i.Item.Status = ERPKeeper.Node.Models.Accounting.ItemStatus.InActive;
                i.ItemGuid = targetItemId;
            });

            erpNodeDBContext.PeriodItemsCOGS.Where(ci => ci.ItemGuid == sourceItemId).ToList().ForEach(i =>
            {
                i.Item.Status = ERPKeeper.Node.Models.Accounting.ItemStatus.InActive;
                i.ItemGuid = targetItemId;
            });
            organization.SaveChanges();

            erpNodeDBContext.Items.Remove(erpNodeDBContext.Items.Find(sourceItemId));
            organization.SaveChanges();
        }

        public Item Copy(Item originalItem)
        {
            var cloneItem = this.erpNodeDBContext.Items
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Uid == originalItem.Uid);

            cloneItem.Uid = Guid.NewGuid();
            cloneItem.PartNumber = "Clone-" + cloneItem.PartNumber;
            cloneItem.Description = "Clone-" + cloneItem.Description;

            this.erpNodeDBContext.Items.Add(cloneItem);
            this.erpNodeDBContext.SaveChanges();

            return cloneItem;
        }

        public Item Save(Item item)
        {
            var existItem = erpNodeDBContext.Items.Find(item.Uid);

            if (existItem == null)
            {
                item.Uid = Guid.NewGuid();
                erpNodeDBContext.Items.Add(item);

                if (item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                    item.PurchaseAccount = item.PurchaseAccount ?? erpNodeDBContext.AccountItems
                        .Where(i => i.SubEnumType == Models.Accounting.AccountSubTypes.Inventory)
                        .FirstOrDefault();

                item.COGSAccount = item.COGSAccount ?? erpNodeDBContext.AccountItems
                    .Where(i => i.SubEnumType == Models.Accounting.AccountSubTypes.CostOfGoodsSold)
                    .FirstOrDefault();

                item.IncomeAccount = item.IncomeAccount ?? erpNodeDBContext.AccountItems
                    .Where(i => i.SubEnumType == Models.Accounting.AccountSubTypes.Income)
                    .FirstOrDefault();

                erpNodeDBContext.SaveChanges();
            }
            else
            {
                existItem.PartNumber = item.PartNumber.Trim();
                existItem.UPC = (item.UPC ?? " ").Trim();
                existItem.Description = item.Description.Trim();
                existItem.UnitPrice = item.UnitPrice;
                existItem.BrandGuid = item.BrandGuid;
                existItem.Status = item.Status;
                existItem.ParentUid = item.ParentUid;
                existItem.PriceRangeUid = item.PriceRangeUid;
                existItem.Specification = item.Specification;
                existItem.OnlineSale = item.OnlineSale;
                existItem.IncomeAccountUid = item.IncomeAccountUid;
                existItem.PurchaseAccountUid = item.PurchaseAccountUid;
                existItem.QuickFinderKey = item.QuickFinderKey;
                existItem.QuickFinderValue = item.QuickFinderValue;

                if (item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                {

                }
                else if (item.ItemType == Models.Items.Enums.ItemTypes.Asset)
                {
                    existItem.FixedAssetTypeUid = item.FixedAssetTypeUid;

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
                Uid = tecItem.Uid,
                PartNumber = tecItem.PartNumber,
                UnitPrice = tecItem.UnitPrice,
                PurchasePrice = tecItem.PurchasePrice,
                ItemType = tecItem.ItemType,
                Description = tecItem.Description,
                OnlineSale = tecItem.OnlineSale,
                IncomeAccountUid = organization.SystemAccounts.Income.Uid,
                PurchaseAccount = erpNodeDBContext.AccountItems
                        .Where(i => i.SubEnumType == Models.Accounting.AccountSubTypes.Inventory)
                        .FirstOrDefault(),

                COGSAccount = erpNodeDBContext.AccountItems
                .Where(i => i.SubEnumType == Models.Accounting.AccountSubTypes.CostOfGoodsSold)
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

            erpNodeDBContext.Configuration.LazyLoadingEnabled = false;
            var Item = erpNodeDBContext.Items.Find(id);
            // erpNodeDBContext.Configuration.LazyLoadingEnabled = true;
            return Item;

        }
        public Item SelectRandom() => this.Query().OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault();
        private Item Create(ItemTypes type, string partNumber, string description, decimal salePrice)
        {
            var newItem = new ERPKeeper.Node.Models.Items.Item()
            {
                Uid = Guid.NewGuid(),
                ItemType = type,
                PartNumber = partNumber,
                Description = description,
                UnitPrice = salePrice
            };

            newItem.IncomeAccountUid = organization.SystemAccounts.Income.Uid;
            newItem.PurchaseAccountUid = organization.SystemAccounts.Expense.Uid;

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
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);
            erpNodeDBContext.SaveChanges();


            sqlCommand = "DELETE FROM [dbo].[ERP_Quotes_Items] WHERE QuoteId IS NULL";
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);
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
            .Where(i => i.ItemType != ItemTypes.Group && i.ParentUid == groupId)
            .ToList();
    }
}
