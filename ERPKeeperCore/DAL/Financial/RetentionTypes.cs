
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;

namespace ERPKeeperCore.Enterprise.DAL.Financial
{
    public class RetentionTypes : ERPNodeDalRepository
    {
        public RetentionTypes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Financial.RetentionType> GetAll()
        {
            return erpNodeDBContext.RetentionTypes.ToList();
        }



        public Models.Financial.RetentionType? Find(Guid Id) => erpNodeDBContext.RetentionTypes.Find(Id);

  
       
    }
}