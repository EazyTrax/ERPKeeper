
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;
using ERPKeeperCore.Enterprise.Models.Financial;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class FundTransfers : ERPNodeDalRepository
    {
        public FundTransfers(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<FundTransfer> ListAll()
        {
            return erpNodeDBContext.FundTransfers.ToList();
        }



        public FundTransfer? Find(Guid Id) => erpNodeDBContext.FundTransfers.Find(Id);


    }
}