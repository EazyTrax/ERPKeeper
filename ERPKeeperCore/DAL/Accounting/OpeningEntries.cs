using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.DAL.Company;
using KeeperCore.ERPNode.DBContext;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.FiscalYears;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore;


namespace KeeperCore.ERPNode.DAL.Accounting
{
    public class OpeningEntries : ERPNodeDalRepository
    {

        public OpeningEntries(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.OpeningEntry;
        }

        public List<Account> GetReadyForPost()
        {
            return organization.ChartOfAccount
.GetAccountsList()
.Where(s => s.PostStatus == LedgerPostStatus.Editable)
.ToList();
        }
        public bool PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();
            Console.WriteLine($"> Post {postingTransactions.Count()} [{this.transactionType}]");

            var trLedger = new TransactionLedger()
            {
                Id = Guid.NewGuid(),
                TrnDate = organization.DataItems.FirstDate.AddDays(-1),
                TransactionName = "Open Entry",
                TransactionNo = 0,
                TransactionType = transactionType
            };
            erpNodeDBContext.TransactionLedgers.Add(trLedger);


            postingTransactions.ForEach(a =>
            {
                trLedger.Debit(a, a.OpeningDebitBalance);
                trLedger.Credit(a, a.OpeningCreditBalance);
                a.PostStatus = LedgerPostStatus.Locked;
            });

            var result = trLedger.FinalValidate();

            if (result == false)
                this.erpNodeDBContext.TransactionLedgers.Remove(trLedger);

            this.erpNodeDBContext.SaveChanges();
            return true;
        }
        public void UnPost()
        {
            Console.WriteLine("> Un Post" + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);


            erpNodeDBContext.AccountItems.ToList()
             .ForEach(s => s.PostStatus = LedgerPostStatus.Editable);

            erpNodeDBContext.SaveChanges();
        }


    }
}