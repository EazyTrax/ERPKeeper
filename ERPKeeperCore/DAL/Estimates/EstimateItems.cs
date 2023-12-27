
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Estimations;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using KeeperCore.ERPNode.Models.Estimations.Enums;

namespace KeeperCore.ERPNode.DAL.Estimates
{
    public class EstimateItems : ERPNodeDalRepository
    {
        public EstimateItems(Organization organization) : base(organization)
        {

        }

        public List<QuoteItem> GetAll() => erpNodeDBContext.EstimateItems.ToList();
        public QuoteItem Find(Guid quoteId) => erpNodeDBContext.EstimateItems.Find(quoteId);

        public void Delete(Guid id)
        {
            var salesQuote = organization.QuoteItems.Find(id);
            erpNodeDBContext.EstimateItems.Remove(salesQuote);
            organization.SaveChanges();
        }

        public void Remove(QuoteItem estimateItem)
        {
            erpNodeDBContext.EstimateItems.Remove(estimateItem);
            organization.SaveChanges();
        }
    }
}