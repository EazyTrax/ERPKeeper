using KeeperCore.ERPNode.DBContext;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Accounting.FiscalYears;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class ItemsCOSG : ERPNodeDalRepository
    {
        public ItemsCOSG(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.ItemCOGS;
        }

        public List<PeriodItemCOGS> GetList() => erpNodeDBContext
            .PeriodItemsCOGS
            .ToList();

        public List<PeriodItemCOGS> GetList(FiscalYear fiscal) => erpNodeDBContext
            .PeriodItemsCOGS
            .Where(p => p.FiscalYearId == fiscal.Id)
            .ToList();

        public List<PeriodItemCOGS> GetList(Item item) => erpNodeDBContext
          .PeriodItemsCOGS
          .Where(i => i.ItemId == item.Id)
          .ToList();



        public void CreateCOSG()
        {
            Console.WriteLine("Create COSG Period");

            organization.FiscalYears
                .GetHistoryList()
                .ForEach(fiscalYear =>
                {
                    Console.WriteLine(" =>" + fiscalYear.Name);
                    this.ClearCOGS(fiscalYear);
                    this.CreateCOSG(fiscalYear);
                    this.UpdateOpeningCOSG(fiscalYear);
                });
        }


        public void CreateCOSG(FiscalYear fiscalYear)
        {
            Console.WriteLine(" + Create Period Cost " + fiscalYear.Name);

            erpNodeDBContext.CommercialItems
                 .Where(ci => ci.Item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                 .Where(ci => ci.Transaction.TrnDate >= fiscalYear.StartDate && ci.Transaction.TrnDate <= fiscalYear.EndDate)
                 .ToList()
                 .ForEach(ci => ci.UpdateInventory());

            var averageCOGSLines = erpNodeDBContext.CommercialItems
                 .Where(ci => ci.Item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                 .Where(ci => ci.Transaction.TrnDate >= fiscalYear.StartDate && ci.Transaction.TrnDate <= fiscalYear.EndDate)
                 .GroupBy(ci => ci.ItemId)
                 .ToList()
                 .Select(ci => new
                 {
                     ItemId = ci.Key,
                     InputAmount = ci.Sum(i => i.InputAmount),
                     InputCost = ci.Sum(i => i.InputValue),
                     OutputAmount = ci.Sum(i => i.OutputAmount),
                 })
                 .ToList();


            var ItemCOSG = fiscalYear.PeriodItemsCOGS.ToList();

            averageCOGSLines.ForEach(a =>
            {
                var itemCosg = ItemCOSG
                .Where(c => c.ItemId == a.ItemId)
                .First();

                itemCosg.InputAmount = a.InputAmount;
                itemCosg.InputValue = a.InputCost;
                itemCosg.OutputAmount = a.OutputAmount;
                itemCosg.LastCalculateDate = DateTime.Now;
            });

            erpNodeDBContext.SaveChanges();
        }


        public void UpdateOpeningCOSG(FiscalYear fiscalYear)
        {
            //First Year, copy opening COSG
            if (fiscalYear.PreviousFiscal == null)
                return;

            var previusFiscalCOSG = fiscalYear.PreviousFiscal.PeriodItemsCOGS.ToList();
            GetList(fiscalYear).ForEach(ItemCOSG =>
            {
                var previusFiscalItemCOSG = previusFiscalCOSG
                .Where(prLine => prLine.ItemId == ItemCOSG.ItemId)
                .FirstOrDefault();

                if (previusFiscalItemCOSG != null)
                {
                    ItemCOSG.OpeningAmount = previusFiscalItemCOSG.RemainAmount;
                    ItemCOSG.OpeningValue = previusFiscalItemCOSG.RemainValue;
                }
            });

            SaveChanges();
        }
        public List<PeriodItemCOGS> GetInventory(DateTime? endDate)
        {

            if (endDate == null)
                endDate = DateTime.Now;

            var AverageItemsCOGSLines = erpNodeDBContext.CommercialItems
                 .Where(ci => ci.Item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                 .Where(ci => ci.Transaction.TrnDate <= endDate)
                 .Include(ci => ci.Item)
                 .GroupBy(ci => ci.ItemId)
                 .ToList()
                 .Select(ci => new
                 {
                     ItemId = ci.Key,
                     ci.First().Item,
                     OutputAmount = ci.Sum(i => i.OutputAmount),
                     InputAmount = ci.Sum(i => i.InputAmount),
                     InputCost = ci.Sum(i => i.InputValue),
                 }).ToList();

            var PeriodItemCOGSList = new List<PeriodItemCOGS>();

            AverageItemsCOGSLines.ForEach(a =>
            {
                var averageItemCOGS = new PeriodItemCOGS()
                {
                    Id = Guid.NewGuid(),
                    ItemId = a.ItemId,
                    Item = a.Item,
                    InputAmount = a.InputAmount,
                    InputValue = a.InputCost,
                    OutputAmount = a.OutputAmount,
                    LastCalculateDate = DateTime.Now
                };

                PeriodItemCOGSList.Add(averageItemCOGS);
            });

            return PeriodItemCOGSList;
        }

        public void ClearCOGS(FiscalYear fiscalYear)
        {
            var periodCost = fiscalYear.PeriodItemsCOGS.ToList();
            periodCost.ForEach(pc => erpNodeDBContext.PeriodItemsCOGS.Remove(pc));
            this.SaveChanges();

            organization.Items.GetInventories.ToList()
                .ForEach(item =>
                {
                    var periodItemCOGS = new PeriodItemCOGS()
                    {
                        Id = Guid.NewGuid(),
                        FiscalYear = fiscalYear,
                        ItemId = item.Id,
                        LastCalculateDate = DateTime.Now
                    };
                    fiscalYear.PeriodItemsCOGS.Add(periodItemCOGS);
                });

            this.SaveChanges();
        }

    }
}
