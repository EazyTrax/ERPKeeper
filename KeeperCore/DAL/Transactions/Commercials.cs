using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KeeperCore.ERPNode.DAL.Transactions
{
    public class Commercials : ERPNodeDalRepository
    {
        public Commercials(Organization organization) : base(organization)
        {

        }

        public IQueryable<Commercial> Query() => erpNodeDBContext.Commercials;

        public List<Commercial> ListAll() => erpNodeDBContext.Commercials
                    .Where(t => t.TransactionType == transactionType)
                    .ToList();

        public List<Commercial> ListOpen => erpNodeDBContext.Commercials
                    .Where(t => t.TransactionType == transactionType)
                    .Where(t => t.Status == CommercialStatus.Open)
                    .ToList();

        public IQueryable<Commercial> QueryOpen => erpNodeDBContext.Commercials
                    .Where(t => t.TransactionType == transactionType)
                    .Where(t => t.Status == CommercialStatus.Open);


        public IQueryable<Commercial> ActiveAndClose => erpNodeDBContext.Commercials
                    .Where(t => t.TransactionType == transactionType);

        public List<Commercial> LastMonth
        {
            get
            {
                var LastMonth = DateTime.Now.AddMonths(-1);
                var lastmonthCommercials = erpNodeDBContext.Commercials
                    .Where(t => t.TransactionType == transactionType)
                    .Where(t => t.TrnDate.Year == LastMonth.Year && t.TrnDate.Month == LastMonth.Month)
                    .OrderBy(tr => tr.No)
                    .ToList();
                return lastmonthCommercials;
            }
        }

        public List<Commercial> ThisMonth
        {
            get
            {
                var ThisMonth = DateTime.Now;
                var lastmonthCommercials = erpNodeDBContext.Commercials
                    .Where(t => t.TransactionType == transactionType)
                    .Where(t => t.TrnDate.Year == ThisMonth.Year && t.TrnDate.Month == ThisMonth.Month)
                    .Where(t => t.Status == CommercialStatus.Open)
                    .ToList();

                return lastmonthCommercials;
            }
        }

        public List<Commercial> GetLastYearOpening()
        {

            var LastYear = DateTime.Now.AddYears(-1);
            var commercials = erpNodeDBContext.Commercials
                .Where(t => t.TransactionType == transactionType)
                .Where(t => t.TrnDate.Year == LastYear.Year)
                .Where(t => t.Status == CommercialStatus.Open)
                .ToList();

            return commercials;

        }


        public List<Commercial> GetLastYear()
        {

            var LastYear = DateTime.Now.AddYears(-1);
            var commercials = erpNodeDBContext.Commercials
                .Where(t => t.TransactionType == transactionType)
                .Where(t => t.TrnDate.Year == LastYear.Year)
                .ToList();

            return commercials;

        }
        public List<Commercial> ThisYear
        {
            get
            {
                var ThisYear = DateTime.Now;
                var lastmonthCommercials = erpNodeDBContext.Commercials
                    .Where(t => t.TransactionType == transactionType)
                    .Where(t => t.TrnDate.Year == ThisYear.Year)
                    .ToList();

                return lastmonthCommercials;
            }
        }

        public List<Commercial> OverDue
        {
            get
            {
                DateTime endDate = DateTime.Now.AddDays(-30);

                var overDueCommercials = ListOpen
                    .Where(t => t.TrnDate <= endDate)
                    .ToList();

                return overDueCommercials;
            }
        }

     

        public Models.Transactions.Commercial Find(Guid id) => erpNodeDBContext.Commercials.Find(id);
        public Models.Transactions.Commercial Find(int transactionNo) => erpNodeDBContext.Commercials.Where(t => t.No == transactionNo).FirstOrDefault();
        public void ChangeStatus(Guid Id, CommercialStatus NewStatus)
        {
            var transaction = this.Find(Id);
            transaction.Status = NewStatus;
            this.SaveChanges();
        }
        public void ChangeAddress(Guid Id, Guid addreessId)
        {
            var _Transaction = this.Find(Id);
            _Transaction.ProfileAddressId = addreessId;
            this.SaveChanges();
        }
        public void UpdatePayments()
        {
            erpNodeDBContext.Commercials
                .ToList()
                .ForEach(c => c.UpdatePaymentStatus());
            erpNodeDBContext.SaveChanges();
        }

        internal void UpdateDoumentsName()
        {
            erpNodeDBContext.Commercials.ToList().ForEach(c =>
            {
                Console.Write("#");
                c.UpdateName();
            });
            erpNodeDBContext.SaveChanges();
        }
    }
}