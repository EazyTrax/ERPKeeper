using ERPKeeper.Node.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ERPKeeper.Node.DAL
{
    public class ERPNodeDalRepository
    {
        public Organization organization { get; private set; }
        protected ERPDbContext erpNodeDBContext { get; set; }
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