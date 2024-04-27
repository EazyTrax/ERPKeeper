
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Items.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class TransactionLedgers : ERPNodeDalRepository
    {
        public TransactionLedgers(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<TransactionLedger> GetAll()
        {
            return erpNodeDBContext.TransactionLedgers.ToList();
        }



        public TransactionLedger? Find(Guid Id) => erpNodeDBContext.TransactionLedgers.Find(Id);


    }
}