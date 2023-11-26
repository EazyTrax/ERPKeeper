
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Reports.CompanyandFinancial;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.DAL.Accounting
{
    public class PreviewAccounts : ERPNodeDalRepository
    {
        public PreviewAccounts(Organization organization) : base(organization)
        {

        }

        public void Remove(Guid id)
        {
            var previewAccountItem = erpNodeDBContext.PreviewAccounts.Find(id);

            if (previewAccountItem != null)
            {
                erpNodeDBContext.PreviewAccounts.Remove(previewAccountItem);
                erpNodeDBContext.SaveChanges();
            }
        }

        public PreviewAccount Find(Guid accountUid, Guid profileUid)
        {
            return erpNodeDBContext.PreviewAccounts
                .Where(p => p.AccountGuid == accountUid && p.OwnerProfileGuid == profileUid)
                .FirstOrDefault();

        }

        public void Create(Guid id, Guid profileUid)
        {
            PreviewAccount newPreviewAccount = new PreviewAccount(id, profileUid);
            erpNodeDBContext.PreviewAccounts.Add(newPreviewAccount);
            erpNodeDBContext.SaveChanges();
        }
    }
}