using ERPKeeperCore.Enterprise.DBContext;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class ERPNodeDalRepository
    {
        public EnterpriseRepo organization { get; set; }
        protected ERPCoreDbContext erpNodeDBContext { get; set; }
        public ERPNodeDalRepository(EnterpriseRepo organization)
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