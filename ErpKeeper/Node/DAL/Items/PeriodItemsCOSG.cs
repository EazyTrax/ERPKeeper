using ERPKeeper.Node.DBContext;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting.FiscalYears;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ERPKeeper.Node.DAL.Items
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
            .Where(p => p.FiscalYearUid == fiscal.Uid)
            .ToList();

        public List<PeriodItemCOGS> GetList(Item item) => erpNodeDBContext
          .PeriodItemsCOGS
          .Where(i => i.ItemGuid == item.Uid)
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
                 .Where(ci => ci.Commercial.TrnDate >= fiscalYear.StartDate && ci.Commercial.TrnDate <= fiscalYear.EndDate)
                 .ToList()
                 .ForEach(ci => ci.UpdateInventory());

            var averageCOGSLines = erpNodeDBContext.CommercialItems
                 .Where(ci => ci.Item.ItemType == Models.Items.Enums.ItemTypes.Inventory)
                 .Where(ci => ci.Commercial.TrnDate >= fiscalYear.StartDate && ci.Commercial.TrnDate <= fiscalYear.EndDate)
                 .GroupBy(ci => ci.ItemGuid)
                 .ToList()
                 .Select(ci => new
                 {
                     ItemGuid = ci.Key,
                     InputAmount = ci.Sum(i => i.InputAmount),
                     InputCost = ci.Sum(i => i.InputValue),
                     OutputAmount = ci.Sum(i => i.OutputAmount),
                 })
                 .ToList();


            var ItemCOSG = fiscalYear.PeriodItemsCOGS.ToList();

            averageCOGSLines.ForEach(a =>
            {
                var itemCosg = ItemCOSG
                .Where(c => c.ItemGuid == a.ItemGuid)
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
                .Where(prLine => prLine.ItemGuid == ItemCOSG.ItemGuid)
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
                 .Where(ci => ci.Commercial.TrnDate <= endDate)
                 .Include(ci => ci.Item)
                 .GroupBy(ci => ci.ItemGuid)
                 .ToList()
                 .Select(ci => new
                 {
                     ItemGuid = ci.Key,
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
                    Uid = Guid.NewGuid(),
                    ItemGuid = a.ItemGuid,
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
                        Uid = Guid.NewGuid(),
                        FiscalYear = fiscalYear,
                        ItemGuid = item.Uid,
                        LastCalculateDate = DateTime.Now
                    };
                    fiscalYear.PeriodItemsCOGS.Add(periodItemCOGS);
                });

            this.SaveChanges();
        }

    }
}
