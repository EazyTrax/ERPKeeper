using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ERPKeeper.Node.Models.Transactions.Commercials;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.Node.Models.Financial.Payments.Enums;

namespace ERPKeeper.Node.DAL.Financial
{
    public class RetentionGroups : ERPNodeDalRepository
    {
        public RetentionGroups(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.RetentionGroup;
        }

        public IQueryable<RetentionGroup> GetQueryable() => erpNodeDBContext.RetentionGroups;
        public List<RetentionGroup> GetAll() => erpNodeDBContext.RetentionGroups.ToList();
        public RetentionGroup Find(Guid id) => erpNodeDBContext.RetentionGroups.Find(id);
        public RetentionGroup Find(RetentionType type, DateTime date, bool created)
        {

            date = new DateTime(date.Year, date.Month, 1);

            var group = erpNodeDBContext.RetentionGroups
                .Where(g => g.RetentionTypeId == type.Uid && g.TrnDate == date)
                .FirstOrDefault();

            if (group == null && created)
                group = Create(type, date);

            erpNodeDBContext.SaveChanges();
            return group;
        }
        public void Reorder()
        {
            this.AutoAssign();

            var paymentGroups = this.GetQueryable()
                    .OrderBy(t => t.TrnDate)
                    .ToList();

            int i = 1;
            foreach (var paymentGroup in paymentGroups)
            {
                paymentGroup.No = i++;
                paymentGroup.Calculate();
                this.erpNodeDBContext.SaveChanges();
            }

        }
        public void AutoAssign()
        {
            erpNodeDBContext.GeneralPayments.Where(p => p.RetentionTypeGuid != null)
                .ToList().ForEach(p =>
                {
                    var group = this.Find(p.RetentionType, p.TrnDate, true);
                    p.RetentionGroup = group;
                    this.erpNodeDBContext.SaveChanges();
                });



        }
        public int GetNextNumber() => (erpNodeDBContext.RetentionGroups.Max(e => (int?)e.No) ?? 0) + 1;
        public RetentionGroup Create(RetentionType type, DateTime date)
        {
            date = new DateTime(date.Year, date.Month, 1);

            var retentionGroup = new RetentionGroup()
            {
                Id = Guid.NewGuid(),
                RetentionType = type,
                RetentionTypeId = type.Uid,
                TrnDate = date,
                No = this.GetNextNumber(),
            };

            erpNodeDBContext.RetentionGroups.Add(retentionGroup);
            erpNodeDBContext.SaveChanges();

            return retentionGroup;
        }
        public RetentionGroup Save(RetentionGroup retentionGroup)
        {
            var existPayment = erpNodeDBContext.RetentionGroups.Find(retentionGroup.Id);

            if (existPayment == null)
                return retentionGroup;

            existPayment.TrnDate = retentionGroup.TrnDate;
            erpNodeDBContext.SaveChanges();
            return existPayment;
        }
    }
}
