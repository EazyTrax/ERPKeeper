
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
    public class PaymentTerms : ERPNodeDalRepository
    {
        public PaymentTerms(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Financial.PaymentTerm> GetAll()
        {
            return erpNodeDBContext.PaymentTerms.ToList();
        }



        public Models.Financial.PaymentTerm? Find(Guid Id) => erpNodeDBContext.PaymentTerms.Find(Id);

  
       
    }
}