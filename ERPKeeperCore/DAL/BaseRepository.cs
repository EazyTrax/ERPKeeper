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
        public ERPNodeDalRepository(Organization organization)
        {
            this.organization = organization;
            this.erpNodeDBContext = organization.ErpCOREDBContext;
        }

        public void SaveChanges()
        {
            erpNodeDBContext.SaveChanges();
        }
    }
}