using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount.Enums;
using KeeperCore.ERPNode.Models.Company;
using KeeperCore.ERPNode.Models.Taxes;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using KeeperCore.ERPNode.Models.Taxes.Enums;

namespace KeeperCore.ERPNode.DAL.Taxes
{
    public class CommercialTaxes : ERPNodeDalRepository
    {
        public CommercialTaxes(Organization organization) : base(organization)
        {
          
        }

        public List<Transaction> GetAssignAbleCommercial() => erpNodeDBContext.Commercials
                .Where(c => c.TaxPeriodId == null && c.TaxCode.isRecoverable)
                .ToList();


        public List<Transaction> GetNonRecoveryCommercial()=>  erpNodeDBContext.Commercials
                .Where(c => c.TaxPeriodId == null && c.TaxCode.isRecoverable == false)
                .ToList();
        
        public List<Transaction> ListCommercialByDirection(TaxDirection? taxDirection, bool recoveryOnly = true)
        {
            IQueryable<Transaction> query = erpNodeDBContext.Commercials;

            if (recoveryOnly)
                query = query.Where(ct => ct.TaxCode.isRecoverable);

            if (taxDirection != null)
                query = query.Where(ct => ct.TaxCode.TaxDirection == taxDirection);

            return query.ToList();

        }
    }
}