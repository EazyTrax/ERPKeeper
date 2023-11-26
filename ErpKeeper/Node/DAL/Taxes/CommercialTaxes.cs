using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Company;
using ERPKeeper.Node.Models.Taxes;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using ERPKeeper.Node.Models.Taxes.Enums;

namespace ERPKeeper.Node.DAL.Taxes
{
    public class CommercialTaxes : ERPNodeDalRepository
    {
        public CommercialTaxes(Organization organization) : base(organization)
        {
          
        }

        public List<Commercial> GetAssignAbleCommercial() => erpNodeDBContext.Commercials
                .Where(c => c.TaxPeriodId == null && c.TaxCode.isRecoverable)
                .ToList();


        public List<Commercial> GetNonRecoveryCommercial()=>  erpNodeDBContext.Commercials
                .Where(c => c.TaxPeriodId == null && c.TaxCode.isRecoverable == false)
                .ToList();
        
        public List<Commercial> ListCommercialByDirection(TaxDirection? taxDirection, bool recoveryOnly = true)
        {
            IQueryable<Commercial> query = erpNodeDBContext.Commercials;

            if (recoveryOnly)
                query = query.Where(ct => ct.TaxCode.isRecoverable);

            if (taxDirection != null)
                query = query.Where(ct => ct.TaxCode.TaxDirection == taxDirection);

            return query.ToList();

        }
    }
}