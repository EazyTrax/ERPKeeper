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
    public class IncomeTaxes : ERPNodeDalRepository
    {
        public IncomeTaxes(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.IncomeTax;
        }

        public List<IncomeTax> GetList()
        {
            return erpNodeDBContext.IncomeTaxs.ToList();
        }
        public IncomeTax Find(Guid id) => erpNodeDBContext.IncomeTaxs.Find(id);
        public IncomeTax Create()
        {
            var taxDate = DateTime.Now;

            var newIncomeTax = new IncomeTax()
            {
                Id = Guid.NewGuid(),
                TrDate = taxDate,
                FiscalYearId = organization.FiscalYears.Find(taxDate).Id,
            };
            erpNodeDBContext.IncomeTaxs.Add(newIncomeTax);
            erpNodeDBContext.SaveChanges();
            return newIncomeTax;

        }
        public bool Delete(Guid id)
        {
            var incomeTax = this.Find(id);
            if (incomeTax.PostStatus != LedgerPostStatus.Locked)
            {
                erpNodeDBContext.IncomeTaxs.Remove(incomeTax);
                this.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool Edit(Guid id)
        {
            var incomeTax = this.Find(id);
            if (incomeTax.PostStatus == LedgerPostStatus.Locked)
            {
                this.UnPost(incomeTax);
                this.SaveChanges();
                return true;
            }
            return false;
        }
        public List<IncomeTax> GetAssignAble(ERPObjectType transactionType, DateTime transactionDate)
        {
            var taxPeriods = erpNodeDBContext.IncomeTaxs
                .Where(s => s.TrDate >= transactionDate)
                .OrderBy(s => s.TrDate)
                .ToList();
            return taxPeriods;
        }
        public IncomeTax Save(Models.Taxes.IncomeTax salesTax)
        {
            var existSalesTax = erpNodeDBContext.IncomeTaxs.Find(salesTax.Id);

            if (existSalesTax.PostStatus == LedgerPostStatus.Locked)
                return existSalesTax;

            existSalesTax.TrDate = new DateTime(salesTax.TrDate.Year, salesTax.TrDate.Month, 1).AddMonths(1).AddDays(-1);
            erpNodeDBContext.SaveChanges();

            return existSalesTax;
        }
        public IncomeTax Create(Guid taxCodeId)
        {
            var newCoperateTax = new IncomeTax()
            {
                Id = Guid.NewGuid(),
                TrDate = DateTime.Now
            };
            erpNodeDBContext.IncomeTaxs.Add(newCoperateTax);
            erpNodeDBContext.SaveChanges();

            return newCoperateTax;
        }
        public void UpdateStartDate()
        {
            Console.WriteLine("> Generate Sales Tax");

            this.GetList()
                .ToList()
                .ForEach(a =>
                {
                    this.Save(a);
                });

            erpNodeDBContext.SaveChanges();
        }
        public List<IncomeTax> GetReadyForPost()
        {
            return erpNodeDBContext.IncomeTaxs
.Where(s => s.PostStatus == LedgerPostStatus.Editable).ToList();
        }
        public List<IncomeTax> GetLedger(IncomeTax taxPeriod) => erpNodeDBContext.IncomeTaxs
            .Where(tr => tr.Id == taxPeriod.Id)
            .ToList();
        public void ReOrder()
        {
            var transactions = erpNodeDBContext.IncomeTaxs
                .OrderBy(t => t.TrDate)
                .ToList();

            int i = 1;
            transactions.ForEach(tr => tr.No = i++);

            erpNodeDBContext.SaveChanges();
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
        public bool PostLedger(IncomeTax tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };

            trLedger.Credit(tr.LiabilityAccount, tr.TaxAmount);
            trLedger.Debit(tr.IncomeTaxExpenAccount, tr.TaxAmount);

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
        public void UnPost(IncomeTax tax)
        {
            if (tax.PostStatus == LedgerPostStatus.Locked)
            {


                organization.LedgersDal.RemoveTransaction(tax.Id);
                tax.PostStatus = LedgerPostStatus.Editable;
                erpNodeDBContext.SaveChanges();
            }
        }
        public void UnPost()
        {
            Console.WriteLine("> Un Post" + ERPObjectType.IncomeTax.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            erpNodeDBContext.IncomeTaxs.ToList().ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }
    }
}