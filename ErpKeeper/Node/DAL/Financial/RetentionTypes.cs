
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Financial.Transfer;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Employees;
using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.Node.Models.Financial.Payments.Enums;

namespace ERPKeeper.Node.DAL.Financial
{
    public class RetentionTypes : ERPNodeDalRepository
    {
        public RetentionTypes(Organization organization) : base(organization)
        {

        }

        public List<RetentionType> GetAll() => erpNodeDBContext.RetentionTypes.ToList();
        public List<RetentionType> GetByDirection(RetentionDirection direction) =>
            erpNodeDBContext.RetentionTypes.Where(r => r.RetentionDirection == direction).ToList();

        public RetentionType Find(Guid id) => erpNodeDBContext.RetentionTypes.Find(id);

        public RetentionType Create(RetentionType type) => erpNodeDBContext.RetentionTypes.Add(type);
        public RetentionType CreateNew(RetentionDirection id)
        {
            var newRetentionType = new RetentionType()
            {
                Uid = Guid.NewGuid(),
                Rate = 0,
                RetentionDirection = id,
            };

            erpNodeDBContext.RetentionTypes.Add(newRetentionType);
            organization.SaveChanges();
            return newRetentionType;
        }
    }
}