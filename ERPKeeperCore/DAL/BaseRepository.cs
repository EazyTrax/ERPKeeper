using KeeperCore.ERPNode.DBContext;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KeeperCore.ERPNode.DAL
{
    public class ERPNodeDalRepository
    {
        public Organization organization { get; private set; }
        protected ERPCoreDbContext erpNodeDBContext { get; set; }
        public Models.Accounting.Enums.ERPObjectType transactionType { get; set; }
        public string trString => this.transactionType.ToString();
        public ERPNodeDalRepository(Organization organization)
        {
            this.organization = organization;
            this.erpNodeDBContext = organization.ErpNodeDBContext;
        }

        public void SaveChanges()
        {
            erpNodeDBContext.SaveChanges();
        }
    }
}