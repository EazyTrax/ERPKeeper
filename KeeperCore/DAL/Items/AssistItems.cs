using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.Items.Enums;
using KeeperCore.ERPNode.Models.Transactions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class AssistItems : ERPNodeDalRepository
    {
        public AssistItems(Organization organization) : base(organization)
        {

        }

        public IQueryable<AssistItem> Query() => erpNodeDBContext.AssistItems;

        public void Delete(Guid? profileId)
        {
            if (profileId == null)
                return;

            var items = erpNodeDBContext.AssistItems
                .Where(ai => ai.ProfileId == profileId)
                .ToList();

            erpNodeDBContext.AssistItems.RemoveRange(items);
            erpNodeDBContext.SaveChanges();
        }

        public void Create(Guid? profileId)
        {
            if (profileId == null)
                return;
            Console.WriteLine($"+ Create item assist. {1}", profileId.ToString());

            this.Delete(profileId);

            var newPopItems = this.erpNodeDBContext
                 .CommercialItems
                 .Where(ci => ci.Commercial.ProfileId == profileId)
                 .GroupBy(ci => ci.ItemId)
                 .Select(i => new { item = i.FirstOrDefault().Item, count = i.Count() })
                 .OrderByDescending(i => i.count)
                 .Take(20)
                 .ToList();

            newPopItems.ForEach(i =>
            {
                var assistItem = new AssistItem()
                {
                    Id = Guid.NewGuid(),
                    ItemId = i.item.Id,
                    ItemPartNumber = i.item.PartNumber,
                    ProfileId = profileId,
                    Count = i.count,
                };
                this.erpNodeDBContext.AssistItems.Add(assistItem);
            });

            erpNodeDBContext.SaveChanges();

            this.UpdatePercentage(profileId);
        }

        private void UpdatePercentage(Guid? profileId)
        {
            var assistItems = this.GetItems(profileId);
            var totalCount = assistItems.Sum(a => a.Count);
            if (totalCount > 0)
                assistItems.ForEach(at => at.Chance = 100 * at.Count / totalCount);

            erpNodeDBContext.SaveChanges();
        }

        public List<AssistItem> GetItems(Guid? profileId)
        {
            if (profileId == null)
                return null;
            return this.erpNodeDBContext.AssistItems
                .Where(a => a.ProfileId == profileId)
                .ToList();
        }
    }
}
