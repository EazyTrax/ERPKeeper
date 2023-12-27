
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.DAL.Company;
using KeeperCore.ERPNode.Models.AccountingEntries;
using KeeperCore.ERPNode.Models.Transactions;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Documents;
using DevExpress.Pdf;
using System.IO;

namespace KeeperCore.ERPNode.DAL.Documents
{

    public class Documents : ERPNodeDalRepository
    {
        public Documents(Organization organization) : base(organization)
        {

        }

        public List<Document> GetAll() => erpNodeDBContext.Documents.ToList();
        public IQueryable<Document> GetQuery() => erpNodeDBContext.Documents;
        public Document Find(Guid id) => erpNodeDBContext.Documents.Find(id);
        public Document CreateNew(Document template)
        {
            template.Id = Guid.NewGuid();
            erpNodeDBContext.Documents.Add(template);
            this.SaveChanges();

            return template;
        }

        public void Delete(Guid id)
        {
            var ExistFile = this.erpNodeDBContext.Documents.Find(id);
            this.erpNodeDBContext.Documents.Remove(ExistFile);
            this.SaveChanges();
        }


   

        public void ReOrder()
        {
            int i = 0;
            erpNodeDBContext.Documents
                .OrderBy(d => d.DocumentDate)
                .ToList()
                .ForEach(d => d.No = ++i);
            organization.SaveChanges();

        }

        public void Create(Document document)
        {
            erpNodeDBContext.Documents.Add(document);
        }
    }
}