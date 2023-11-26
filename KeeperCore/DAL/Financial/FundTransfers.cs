
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore;

namespace KeeperCore.ERPNode.DAL.Financial
{
    public class FundTransfers : ERPNodeDalRepository
    {
        public FundTransfers(Organization organization) : base(organization)
        {
            transactionType = Models.Accounting.Enums.ERPObjectType.FundTransfer;
        }

        public List<FundTransfer> GetReadyForPost()
        {
            return erpNodeDBContext.FundTransfers
.Where(s => s.PostStatus == LedgerPostStatus.Editable)
.ToList();
        }
        public List<FundTransfer> GetAll()
        {
            return erpNodeDBContext.FundTransfers.ToList();
        }
        public IQueryable<FundTransfer> Query() => erpNodeDBContext.FundTransfers;

        private int GetNextNumber()
        {
            return (erpNodeDBContext.FundTransfers.Max(e => (int?)e.No) ?? 0) + 1;
        }

        public void ReOrder()
        {
            var transfers = erpNodeDBContext.FundTransfers
                .OrderBy(t => t.TrnDate).ThenBy(t => t.No)
                .ToList();

            int i = 0;
            foreach (var transfer in transfers)
            {
                transfer.No = ++i;
            }

            erpNodeDBContext.SaveChanges();

        }
        public void Add(FundTransfer entity)
        {
            erpNodeDBContext.FundTransfers.Add(entity);
            erpNodeDBContext.SaveChanges();
        }
        public void Delete(FundTransfer transfer)
        {
            if (transfer.PostStatus == LedgerPostStatus.Locked)
            {
                this.UnPost(transfer);
                erpNodeDBContext.FundTransfers.Remove(transfer);
                erpNodeDBContext.SaveChanges();
            }
        }
        public FundTransfer Find(Guid transactionId)
        {
            return erpNodeDBContext.FundTransfers.Find(transactionId);
        }
        public void Save(FundTransfer transfer)
        {
            var existTransfer = erpNodeDBContext.FundTransfers.Find(transfer.Id);

            if (existTransfer == null)
            {
                transfer.FiscalYear = organization.FiscalYears.Find(transfer.TrnDate);
                transfer.TransactionType = Models.Accounting.Enums.ERPObjectType.FundTransfer;
                transfer.No = GetNextNumber();
                transfer.Reference = "";
                transfer.BankFeeAccountId = organization.SystemAccounts.BankFee.Id;

                erpNodeDBContext.FundTransfers.Add(transfer);
            }
            else
            {
                if (existTransfer.PostStatus == LedgerPostStatus.Locked)
                    return;

                transfer.FiscalYear = organization.FiscalYears.Find(transfer.TrnDate);
                existTransfer.Reference = transfer.Reference;
                existTransfer.TrnDate = transfer.TrnDate;
                existTransfer.DepositAccountId = transfer.DepositAccountId;
                existTransfer.WithDrawAccountId = transfer.WithDrawAccountId;
                existTransfer.AmountwithDraw = transfer.AmountwithDraw;
                existTransfer.AmountFee = transfer.AmountFee;
                existTransfer.Reference = transfer.Reference;
            }



            erpNodeDBContext.SaveChanges();
        }
        public bool Delete(Guid id)
        {
            var Transfer = erpNodeDBContext.FundTransfers.Find(id);

            if (Transfer.PostStatus != LedgerPostStatus.Locked)
            {
                erpNodeDBContext.FundTransfers.Remove(Transfer);
                erpNodeDBContext.SaveChanges();
                return true;
            }
            return false;
        }
        public FundTransfer Copy(FundTransfer originalFundTransfer, DateTime trDate)
        {
            var cloneFundTransfer = this.erpNodeDBContext.FundTransfers
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == originalFundTransfer.Id);

            cloneFundTransfer.Id = Guid.NewGuid();
            cloneFundTransfer.TrnDate = trDate;
            cloneFundTransfer.Reference = "Clone-" + cloneFundTransfer.Reference;
            cloneFundTransfer.No = organization.FundTransfers.GetNextNumber();
            cloneFundTransfer.PostStatus = LedgerPostStatus.Editable;

            this.erpNodeDBContext.FundTransfers.Add(cloneFundTransfer);
            this.erpNodeDBContext.SaveChanges();

            return cloneFundTransfer;
        }
        public bool PostLedger(FundTransfer tr, bool SaveImmediately = true)
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

            trLedger.Credit(tr.WithDrawAccount, tr.AmountwithDraw);
            if (tr.AmountFee > 0)
                trLedger.Debit(tr.BankFeeAccount, tr.AmountFee);
            trLedger.Debit(tr.DepositAccount, tr.AmountDeposit);




            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }
            else
            {
                return false;
            }

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
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
        public void UnPost(FundTransfer transfer)
        {
            Console.WriteLine("> UnPost GL," + transfer.TransactionType + " " + transfer.No);
            organization.LedgersDal.RemoveTransaction(transfer.Id);

            transfer.PostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
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

            erpNodeDBContext.FundTransfers.ToList().ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }
    }
}