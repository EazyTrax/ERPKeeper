
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Financial;


namespace KeeperCore.ERPNode.DAL.Financial
{
    public class Loans : ERPNodeDalRepository
    {
        public Loans(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.Loan;
        }

        public List<Loan> GetAll()
        {
            return erpNodeDBContext.Loans.ToList();
        }
        public List<Loan> GetReadyForPost()
        {
            return erpNodeDBContext.Loans.Where(s => s.PostStatus == LedgerPostStatus.Editable).ToList();
        }
        public Loan Find(Guid id) => erpNodeDBContext.Loans.Find(id);
        private int GetNextNumber()
        {
            return (erpNodeDBContext.Loans.Max(e => (int?)e.No) ?? 0) + 1;
        }
        public void Add(Loan entity) => erpNodeDBContext.Loans.Add(entity);
        public void ReOrder()
        {
            var loans = erpNodeDBContext.Loans
                .OrderBy(t => t.TrnDate)
                .ToList();

            int i = 1;
            foreach (var loan in loans)
            {
                loan.No = i;
                i++;

            }

            erpNodeDBContext.SaveChanges();
        }
        public void Save(Loan loan)
        {
            var existLoan = erpNodeDBContext.Loans.Find(loan.Id);

            if (existLoan == null)
            {
                loan.FiscalYear = organization.FiscalYears.Find(loan.TrnDate);
                loan.TransactionType = Models.Accounting.Enums.ERPObjectType.Loan;
                erpNodeDBContext.Loans.Add(loan);
            }
            else
            {
                if (existLoan.PostStatus == LedgerPostStatus.Locked)
                {
                    existLoan.FiscalYear = organization.FiscalYears.Find(existLoan.TrnDate);
                    existLoan.TrnDate = loan.TrnDate;
                    existLoan.DebitTo_AssetAccountId = loan.DebitTo_AssetAccountId;
                    existLoan.CreditTo_LiabilityAccountId = loan.CreditTo_LiabilityAccountId;
                    existLoan.Amount = loan.Amount;
                }
            }

            erpNodeDBContext.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var tr = erpNodeDBContext.Loans.Find(id);
            erpNodeDBContext.Loans.Remove(tr);
            erpNodeDBContext.SaveChanges();
        }
        public void UnPost()
        {
            Console.WriteLine("> Un Post " + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "UPDATE [dbo].[ERP_Finance_Loans] SET  [PostStatus] = '0'";
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
        }
        public void PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();
            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;

            postingTransactions.ForEach(s =>
            {
                this.PostLedger(s, false);
            });

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        public void UnPost(Loan loan)
        {
            organization.LedgersDal.RemoveTransaction(loan.Id);
            loan.PostStatus = LedgerPostStatus.Editable;
        }
        public bool PostLedger(Loan tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };


            trLedger.Debit(tr.DebitTo_AssetAccount, tr.Amount);
            trLedger.Credit(tr.CreditTo_LiabilityAccount, tr.Amount);

            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();
            return true;
        }

    }
}