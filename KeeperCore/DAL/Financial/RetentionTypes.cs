
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Employees;
using KeeperCore.ERPNode.Models.Financial.Payments;
using KeeperCore.ERPNode.Models.Financial.Payments.Enums;
using Microsoft.EntityFrameworkCore;

namespace KeeperCore.ERPNode.DAL.Financial
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

        public void Create(RetentionType type)
        {
            erpNodeDBContext.RetentionTypes.Add(type);
        }

        public RetentionType CreateNew(RetentionDirection id)
        {
            var newRetentionType = new RetentionType()
            {
                Id = Guid.NewGuid(),
                Rate = 0,
                RetentionDirection = id,
            };

            erpNodeDBContext.RetentionTypes.Add(newRetentionType);
            organization.SaveChanges();
            return newRetentionType;
        }
    }
}